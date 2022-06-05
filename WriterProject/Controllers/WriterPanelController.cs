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
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel

        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());


        public ActionResult WriterProfile()
        {
            return View();
        }


        public ActionResult MyHeading()
        {

            string writerMail = Session["WriterMail"].ToString();

            int id = writerManager.GetWriterByMail(writerMail).WriterID;

            var values = headingManager.GetListByWriter(id).Where(m => m.HeadingStatus == true).ToList();

            return View(values);
          
        }

        [HttpGet]
        public ActionResult NewHeading()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            GetCategories();
            return View();

        }


        [HttpPost]
        public ActionResult NewHeading(Heading heading)
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

                return RedirectToAction("NewHeading", "WriterPanel");

            }


            HeadingValidator validation = new HeadingValidator();
            ValidationResult validationResult = validation.Validate(heading);

            if (validationResult.IsValid)
            {

                string writerMail = Session["WriterMail"].ToString();
                heading.HeadingDate = DateTime.Now;
                heading.HeadingStatus = true;
                heading.WriterID = writerManager.GetWriterByMail(writerMail).WriterID;
                headingManager.TAdd(heading);
                return RedirectToAction("MyHeading");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }


            }

            GetCategories();
            return View();

        }


        protected void GetCategories()
        {


            List<SelectListItem> categoriesList = (from x in categoryManager.TGetList().Where(m => m.CategoryStatus == true)
                                                   select new SelectListItem
                                                   {


                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()


                                                   }).ToList();

            ViewBag.categories = categoriesList;

        }


        [HttpGet]
        public ActionResult EditHeading(int id)
        {


            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            var value = headingManager.TGetByID(id);


            GetCategories();
            return View(value);

        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
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

                return RedirectToAction("NewHeading", "WriterPanel");

            }

            HeadingValidator validation = new HeadingValidator();
            ValidationResult validationResult = validation.Validate(heading);

            if (validationResult.IsValid)
            {

                var value = headingManager.TGetByID(heading.HeadingID);

                value.Category = heading.Category;                
                value.HeadingDate = DateTime.Now;
                value.HeadingName = heading.HeadingName;


                headingManager.TUpdate(value);
                return RedirectToAction("MyHeading");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }


            }

            GetCategories();
         
            return View();


        }


        public ActionResult RemoveHeading(int id)
        {

            var value = headingManager.TGetByID(id);

            value.HeadingStatus = false;

            headingManager.TUpdate(value);

            return RedirectToAction("MyHeading");

        }


        public ActionResult AllHeadings()
        {

            var headings = headingManager.TGetList();


            return View(headings);

        }





    }
}