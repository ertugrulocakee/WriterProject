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
    [Authorize(Roles="2")]
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage

        MessageManager messageManager = new MessageManager(new EFMessageDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Inbox(int page = 1)
        {

            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetReceiveBox(writerMail).Where(m => m.MessageStatus == true).ToPagedList(page,5);

            return View(messageList);

        }

        public ActionResult SendBox(int page = 1)
        {

            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == true).ToPagedList(page, 5);

            return View(messageList);

        }


        public PartialViewResult MessageListMenu()
        {

            MessageManager messageManager = new MessageManager(new EFMessageDAL());

            string writerMail = Session["WriterMail"].ToString();

            ViewBag.messageInboxCount = messageManager.GetReceiveBox(writerMail).Where(m => m.MessageStatus == true).Count();

            ViewBag.messageSendBoxCount = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == true).Count();

            ViewBag.removedSendMessagesCount = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == false).Count();


            return PartialView();

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

        public ActionResult RemovedSendMessages(int page = 1)
        {
            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == false).ToPagedList(page, 5);

            return View(messageList);

        }


        [HttpGet]
        public ActionResult NewMessage()
        {


            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            GetWriters();
            return View();

        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {

            if (message.ReceiverMail == null)
            {

                TempData["message"] = "Bir yazar secilmelidir!";

                return RedirectToAction("NewMessage", "WriterPanelMessage");

            }


            MessageValidator messageValidator = new MessageValidator();
            ValidationResult validationResult = messageValidator.Validate(message);

            if (validationResult.IsValid)
            {

                string writerMail = Session["WriterMail"].ToString();

                if (writerMail.Contains(message.ReceiverMail))
                {

                    TempData["message"] = "Kendine mesaj atamazsin!";

                    return RedirectToAction("NewMessage", "WriterPanelMessage");

                }

                message.SenderMail = writerMail;
                message.MessageStatus = true;
                message.Date = DateTime.Now;
                messageManager.TAdd(message);
                return RedirectToAction("SendBox");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }

            }

            GetWriters();
            return View();


        }


        protected void GetWriters()
        {

            List<SelectListItem> writersList = (from x in writerManager.TGetList().Where(m=>m.WriterStatus==true)
                                               select new SelectListItem
                                               {


                                                   Text = x.WriterMail,
                                                   Value = x.WriterMail


                                               }).ToList();


            writersList.Insert(0, new SelectListItem()
            {
                Text = "---Yazar Seçin---",
                Value = String.Empty

            });


            ViewBag.writers = writersList;


        }


    }
}