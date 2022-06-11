using BL.Concrete;
using BL.Validation;
using DAL.EF;
using Entities.Concrete;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


        [HttpGet]
        public ActionResult HomePage()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            return View();

        }

        public PartialViewResult CountsArea()
        {

            WriterManager writerManager = new WriterManager(new EFWriterDAL());
            HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
            CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
            ContentManager contentManager = new ContentManager(new EFContentDAL());

            ViewBag.writerCount = writerManager.TGetList().Where(m => m.WriterStatus == true).Count();
            ViewBag.headingCount = headingManager.TGetList().Where(m => m.HeadingStatus == true).Count();
            ViewBag.categoryCount = categoryManager.TGetList().Where(m => m.CategoryStatus == true).Count();
            ViewBag.contentCount = contentManager.TGetList().Where(m => m.ContentStatus == true).Count();



            return PartialView();

        }


        public PartialViewResult ContactArea()
        {


            return PartialView();

        }


        [HttpPost]
        public ActionResult HomePage(Contact contact)
        {

            ContactManager contactManager = new ContactManager(new EFContactDAL());

            ContactValidator contactValidator = new ContactValidator();
            ValidationResult validationResult = contactValidator.Validate(contact);

            if (validationResult.IsValid)
            {


                contact.ContactStatus = true;
                contact.Date = DateTime.Now;
                contactManager.TAdd(contact);

                TempData["message"] = "Mesaj basariyla iletildi!";

                return RedirectToAction("HomePage");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                    TempData["message"] += item.ErrorMessage;

                }

            }


            return RedirectToAction("HomePage");

        }

        public PartialViewResult Testimonials()
        {

            TestimonialManager testimonialManager = new TestimonialManager(new EFTestimonialDAL());

            var testimonials = testimonialManager.TGetList().Where(m => m.TestimonialStatus == true).ToList();

            return PartialView(testimonials);

        }

    }
}