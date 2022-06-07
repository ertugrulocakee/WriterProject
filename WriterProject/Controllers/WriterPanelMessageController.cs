﻿using BL.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage

        MessageManager messageManager = new MessageManager(new EFMessageDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Inbox()
        {

            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetReceiveBox(writerMail).Where(m => m.MessageStatus == true).ToList();

            return View(messageList);

        }

        public ActionResult SendBox()
        {

            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == true).ToList();

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

        public ActionResult RemovedSendMessages()
        {
            string writerMail = Session["WriterMail"].ToString();

            var messageList = messageManager.GetSendBox(writerMail).Where(m => m.MessageStatus == false).ToList();

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

                string writerMail = Session["WriterMail"].ToString();
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


            return View();


        }


    }
}