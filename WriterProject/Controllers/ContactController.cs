using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{

    [Authorize(Roles = "1")]
    public class ContactController : Controller
    {
        // GET: Contact

        ContactManager contactManager = new ContactManager(new EFContactDAL());
        public ActionResult Index()
        {
            var contactValues = contactManager.TGetList().Where(m=>m.ContactStatus==true).ToList();

            return View(contactValues);

        }


        public ActionResult GetContactDetails(int id)
        {

            var contactValues = contactManager.TGetByID(id);

            return View(contactValues);

        }

        public PartialViewResult MessagePartial()
        {

            MessageManager messageManager = new MessageManager(new EFMessageDAL());

            string email = Session["AdminEmail"].ToString();

            ViewBag.contactMessagesCount = contactManager.TGetList().Where(m => m.ContactStatus == true).Count();

            ViewBag.messageInboxCount = messageManager.GetReceiveBox(email).Where(m => m.MessageStatus == true).Count();

            ViewBag.messageSendBoxCount = messageManager.GetSendBox(email).Where(m => m.MessageStatus == true).Count();

            ViewBag.removedContactMessagesCount = contactManager.TGetList().Where(m => m.ContactStatus == false).Count();

            ViewBag.removedSendMessagesCount = messageManager.GetSendBox(email).Where(m => m.MessageStatus == false).Count();    

            return PartialView();

        }


        public ActionResult DeleteContact(int id)
        {

            var value = contactManager.TGetByID(id);    

            value.ContactStatus = false;

            contactManager.TUpdate(value);

            return RedirectToAction("Index");

        }


        public ActionResult RemovedContactMessages()
        {

            var contactValues = contactManager.TGetList().Where(m => m.ContactStatus == false).ToList();

            return View(contactValues);

        }


        public ActionResult BringBackContactMessage(int id)
        {

            var value = contactManager.TGetByID(id);

            value.ContactStatus = true;

            contactManager.TUpdate(value);

            return RedirectToAction("Index");

        }


    }
}