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

namespace WriterProject.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message

        MessageManager messageManager = new MessageManager(new EFMessageDAL());


        public ActionResult Inbox()
        {

            var messageList = messageManager.GetReceiveBox().Where(m => m.MessageStatus == true).ToList();

            return View(messageList);

        }

        public ActionResult SendBox()
        {

            var messageList = messageManager.GetSendBox().Where(m => m.MessageStatus == true).ToList();

            return View(messageList);

        }


        [HttpGet]
        public ActionResult NewMessage()
        {


            return View();  
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {


            MessageValidator messageValidator = new MessageValidator();
            ValidationResult validationResult = messageValidator.Validate(message);

            if (validationResult.IsValid)
            {
                message.MessageStatus = true;
                message.Date = DateTime.Now;
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


        public ActionResult RemovedSendMessages()
        {

            var messageList = messageManager.GetSendBox().Where(m => m.MessageStatus == false).ToList();

            return View(messageList);

        }


       

    }
}