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
    public class CategoryController : Controller
    {
        // GET: Category

        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());

        [Authorize(Roles="1")]
        public ActionResult Index()
        {
            var categories = categoryManager.TGetList().Where(x=>x.CategoryStatus == true).ToList();
            return View(categories);
        }


        [HttpGet]
        public ActionResult AddCategory()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(category);


            if (validationResult.IsValid)
            {
                category.CategoryStatus = true;
                categoryManager.TAdd(category);
                return RedirectToAction("Index");

            }
            else
            {

                foreach(var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }

                return View();

            }



        }


        public ActionResult DeleteCategory(int id)
        {

            var category = categoryManager.TGetByID(id);
            
            category.CategoryStatus = false;    

            categoryManager.TUpdate(category);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {

            var category = categoryManager.TGetByID(id);

            return View(category);

        }


        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(category);


            if (validationResult.IsValid)
            {
                var value = categoryManager.TGetByID(category.CategoryID);

                value.CategoryName = category.CategoryName;
                value.CategoryDescription = category.CategoryDescription;   

                categoryManager.TUpdate(value);
                return RedirectToAction("Index");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }

                return View();

            }



        }

        public ActionResult Test()
        {

            return View();

        }
        

    }
}