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
    public class HeadingController : Controller
    {
        // GET: Heading

        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Index()
        {
            var values = headingManager.TGetList().Where(m=>m.HeadingStatus == true).ToList();

            return View(values);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];  

            }

            Categories();
            Writers();

            return View();

        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {

            try
            {

                var category = categoryManager.TGetByID(heading.Category.CategoryID);
                heading.Category = category;

            }
            catch(Exception ex) 
            {

                Console.WriteLine(ex);

                TempData["message"] = "Secilecek kategori yok! Lutfen once kategori olusturun!";

                return RedirectToAction("AddHeading","Heading");   

            }


            try
            {

                var writer = writerManager.TGetByID(heading.Writer.WriterID);
                heading.Writer = writer;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                TempData["message"] = "Secilecek yazar yok! Lutfen once yazar olusturun!";

                return RedirectToAction("AddHeading", "Heading");

            }


            HeadingValidator  validation = new HeadingValidator();
            ValidationResult validationResult = validation.Validate(heading);

            if(validationResult.IsValid)
            {

                heading.HeadingDate = DateTime.Now;
                heading.HeadingStatus = true;
                headingManager.TAdd(heading);
                return RedirectToAction("Index");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
              
                }


            }

            Categories();
            Writers();
            return View();

        }


        protected void Categories()
        {

            List<SelectListItem> categoriesList = (from x in categoryManager.TGetList().Where(m=>m.CategoryStatus==true)
                                                   select new SelectListItem
                                                   {


                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()


                                                   }).ToList();

            ViewBag.categories = categoriesList;

         
        }

        protected void Writers()
        {
            List<SelectListItem> writersList = (from x in writerManager.TGetList().Where(m => m.WriterStatus == true)
                                                   select new SelectListItem
                                                   {


                                                       Text = x.WriterName+" "+x.WriterSurName,
                                                       Value = x.WriterID.ToString()


                                                   }).ToList();

            ViewBag.writers = writersList;




        }


        public ActionResult DeleteHeading(int id)
        {

            var value = headingManager.TGetByID(id);

            value.HeadingStatus = false;

            headingManager.TUpdate(value);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateHeading(int id)
        {

            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"];

            }

            var value = headingManager.TGetByID(id);

            Writers();

            Categories();


            return View(value);

        }

        [HttpPost]
        public ActionResult UpdateHeading(Heading heading)
        {

            try
            {

                var category = categoryManager.TGetByID(heading.Category.CategoryID);
                heading.Category = category;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                TempData["message"] = "Secilecek kategori yok! Lutfen once kategori olusturun!";

                return RedirectToAction("AddHeading", "Heading");

            }


            try
            {

                var writer = writerManager.TGetByID(heading.Writer.WriterID);
                heading.Writer = writer;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                TempData["message"] = "Secilecek yazar yok! Lutfen once yazar olusturun!";

                return RedirectToAction("AddHeading", "Heading");

            }


            HeadingValidator validation = new HeadingValidator();
            ValidationResult validationResult = validation.Validate(heading);

            if (validationResult.IsValid)
            {

                var value = headingManager.TGetByID(heading.HeadingID);

                value.Category = heading.Category;
                value.Writer = heading.Writer;
                value.HeadingDate = DateTime.Now;
                value.HeadingName = heading.HeadingName;
                
                
                headingManager.TUpdate(value);
                return RedirectToAction("Index");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }


            }

            Categories();
            Writers();
            return View();


        }


        public ActionResult HeadingReport()
        {
            var values = headingManager.TGetList().Where(m => m.HeadingStatus == true).ToList();

            return View(values);

        }


    }
}