using BL.Concrete;
using BL.Validation;
using DAL.EF;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WriterProject.Controllers
{

    [Authorize(Roles = "1")]
    public class MessageController : Controller
    {
        // GET: Message

        MessageManager messageManager = new MessageManager(new EFMessageDAL());
        AdminManager adminManager = new AdminManager(new EFAdminDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL()); 


        public ActionResult Inbox(int page = 1)
        {

            string email = Session["AdminEmail"].ToString();

            var messageList = messageManager.GetReceiveBox(email).Where(m => m.MessageStatus == true).ToPagedList(page,5);

            return View(messageList);

        }

        public ActionResult SendBox(int page = 1)
        {

            string email = Session["AdminEmail"].ToString();

            var messageList = messageManager.GetSendBox(email).Where(m => m.MessageStatus == true).ToPagedList(page,5);

            return View(messageList);

        }


        [HttpGet]
        public ActionResult NewMessage()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }
      
            GetAdmins();

            return View();  

        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {

              
            if(message.ReceiverMail == null)
            {

                TempData["message"] = "Bir admin secilmelidir!";

                return RedirectToAction("NewMessage", "Message");

            }
       

            MessageValidator messageValidator = new MessageValidator();
            ValidationResult validationResult = messageValidator.Validate(message);

            if (validationResult.IsValid)
            {

                string email = Session["AdminEmail"].ToString();

                if (email.Contains(message.ReceiverMail))
                {

                    TempData["message"] = "Kendine mesaj atamazsin!";

                    return RedirectToAction("NewMessage", "Message");

                }

                message.MessageStatus = true;
                message.Date = DateTime.Now;
                message.SenderMail = email;
                messageManager.TAdd(message);
                return RedirectToAction("SendBox");

            }
            else
            {

                foreach(var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }

            }


            GetAdmins();
            return View();

        }


        public ActionResult GetMessageDetails(int id)
        {

            var messageValues = messageManager.TGetByID(id);

            return View(messageValues);

        }

    
        public ActionResult GetSendMessageDetails(int id)
        {


            var messageValues = messageManager.TGetByID(id);

            return View(messageValues);

        }


        public ActionResult DeleteMessage(int id)
        {

            var value = messageManager.TGetByID(id);

            value.MessageStatus = false;

            messageManager.TUpdate(value);

            return RedirectToAction("SendBox");

        }


        public ActionResult RemovedSendMessages(int page = 1)
        {

            string email = Session["AdminEmail"].ToString();

            var messageList = messageManager.GetSendBox(email).Where(m => m.MessageStatus == false).ToPagedList(page,5);

            return View(messageList);

        }


        protected void GetAdmins()
        {

            List<SelectListItem> adminsList = (from x in adminManager.TGetList()
                                    select new SelectListItem
                                    {


                                        Text = x.email,
                                        Value = x.email


                                    }).ToList();


            adminsList.Insert(0, new SelectListItem()
            {
                Text = "---Admin Seçin---",
                Value = String.Empty

            });


            ViewBag.admins = adminsList;    

        }


    }
}