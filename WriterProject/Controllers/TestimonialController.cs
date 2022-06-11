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

    [Authorize(Roles="2")]
    public class TestimonialController : Controller
    {
        // GET: Testimonial

        TestimonialManager testimonialManager = new TestimonialManager(new EFTestimonialDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        [HttpGet]
        public ActionResult MyTestimonial()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];  

            }


            var writer = writerManager.GetWriterByMail(Session["WriterMail"].ToString());

            var testimonial = testimonialManager.GetTestimonialByWriter(writer.WriterID);

            if(testimonial != null)
            {

                return View(testimonial);

            }
            else
            {


                return RedirectToAction("AddTestimonial", "Testimonial");

            }

        }

        [HttpPost]
        public ActionResult MyTestimonial(Testimonial testimonial)
        {

            TestimonialValidator testimonialValidator = new TestimonialValidator();
            ValidationResult result = testimonialValidator.Validate(testimonial);

            if (result.IsValid)
            {

                var value = testimonialManager.TGetByID(testimonial.TestimonialID);

                value.TestimonialValue = testimonial.TestimonialValue;

                testimonialManager.TUpdate(value);

                TempData["message"] = "Referans guncellendi!";

                return RedirectToAction("MyTestimonial");


            }
            else
            {

                foreach (var item in result.Errors)
                {

                    TempData["message"] += item.ErrorMessage;

                }

            }

            return RedirectToAction("MyTestimonial");  

        }


        public ActionResult RemoveTestimonial(int id)
        {

            var testimonial = testimonialManager.TGetByID(id);

            testimonial.TestimonialStatus = false;

            testimonialManager.TUpdate(testimonial);

            TempData["message"] = "Referans silindi!";

            return RedirectToAction("MyTestimonial");

        }

        [HttpGet]
        public ActionResult AddTestimonial()
        {

            return View();

        }


        [HttpPost]
        public ActionResult AddTestimonial(Testimonial testimonial)
        {

            TestimonialValidator testimonialValidator = new TestimonialValidator();
            ValidationResult result = testimonialValidator.Validate(testimonial);

            if (result.IsValid)
            {

                testimonial.WriterID = writerManager.GetWriterByMail(Session["WriterMail"].ToString()).WriterID;

                testimonial.TestimonialStatus = true;

                testimonialManager.TAdd(testimonial);

                TempData["message"] = "Referans olusturuldu!";

                return RedirectToAction("MyTestimonial");

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


   
    }
}