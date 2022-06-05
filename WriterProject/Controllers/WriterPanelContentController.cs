using BL.Concrete;
using DAL.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent

        ContentManager contentManager = new ContentManager(new EFContentDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult MyContent()
        {

            string writerMail = Session["WriterMail"].ToString();

            int id = writerManager.GetWriterByMail(writerMail).WriterID;

            var contentValues = contentManager.GetListByWriter(id).Where(m => m.ContentStatus == true).ToList();
            return View(contentValues);

        }

        [HttpGet]
        public ActionResult AddContent()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {

            string writerMail = Session["WriterMail"].ToString();

            int id = writerManager.GetWriterByMail(writerMail).WriterID;

            content.ContentDate = DateTime.Now;
            content.WriterID = id;
            content.ContentStatus = true;
            contentManager.TAdd(content);
            return RedirectToAction("MyContent");

        }

    }
}