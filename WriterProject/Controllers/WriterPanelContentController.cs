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
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace WriterProject.Controllers
{
    [Authorize(Roles="2")] 
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent

        ContentManager contentManager = new ContentManager(new EFContentDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());
        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());

        public ActionResult MyContent(string p , int page = 1)
        {

            string writerMail = Session["WriterMail"].ToString();

            int id = writerManager.GetWriterByMail(writerMail).WriterID;

            var contentValues = contentManager.GetListByWriter(id);

            if (p != null)
            {

               contentValues = contentValues.Where(m => m.ContentStatus == true && m.ContentValue.Contains(p)).ToList();

            }
            else
            {
              contentValues = contentValues.Where(m => m.ContentStatus == true).ToList();

            }
           
            return View(contentValues.ToPagedList(page,10));

        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.Id = id;    

            var heading = headingManager.TGetByID(id);

            ViewBag.Name = heading.HeadingName;

            return View();

        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {

            ContentValidator contentValidator = new ContentValidator();
            ValidationResult result = contentValidator.Validate(content);

            if (result.IsValid)
            {

                string writerMail = Session["WriterMail"].ToString();

                int id = writerManager.GetWriterByMail(writerMail).WriterID;

                content.ContentDate = DateTime.Now;
                content.WriterID = id;
                content.ContentStatus = true;
                contentManager.TAdd(content);
                return RedirectToAction("MyContent");

            }
            else
            {

                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }


            }

            return View();

        }


        public ActionResult ContentByHeader(int id , int page = 1)
        {
            var contentValues = contentManager.GetListByID(id).Where(m => m.ContentStatus == true).ToPagedList(page,5);
            return View(contentValues);

        }


    }
}