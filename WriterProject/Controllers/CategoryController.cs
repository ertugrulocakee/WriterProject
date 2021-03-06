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
using PagedList;
using PagedList.Mvc;

namespace WriterProject.Controllers
{

    [Authorize(Roles = "1")]
    public class CategoryController : Controller
    {
        // GET: Category

        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
        AdminManager adminManager = new AdminManager(new EFAdminDAL());

       
        public ActionResult Index(int page = 1)
        {
            var categories = categoryManager.TGetList().Where(x => x.CategoryStatus == true).ToPagedList(page, 5);
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


                var categories = categoryManager.GetCategoriesByCategoryName(category.CategoryName);

                if (categories.Any())
                {

                    ViewBag.Message = "Boyle bir kategori zaten mevcut!";

                    return View();

                }
                else
                {

                    category.CategoryStatus = true;
                    categoryManager.TAdd(category);
                    return RedirectToAction("Index");

                }

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

                var categories = categoryManager.GetCategoriesByCategoryName(category.CategoryName).Where(m=>m.CategoryID != category.CategoryID);


                if (categories.Any())
                {
                    ViewBag.Message = "Boyle bir kategori zaten mevcut!";

                    return View(category);

                }
                else
                {


                    var value = categoryManager.TGetByID(category.CategoryID);

                    value.CategoryName = category.CategoryName;
                    value.CategoryDescription = category.CategoryDescription;

                    categoryManager.TUpdate(value);
                    return RedirectToAction("Index");
                }

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

        public ActionResult CategoryHeadings(int id , int page=1)
        {

          var headings = headingManager.GetListByCategory(id).Where(x=>x.HeadingStatus == true).ToPagedList(page,5);

          return View(headings);

        }


       public PartialViewResult AdminUserName()
       {

            ViewBag.admin = adminManager.GetAdmin(Session["AdminEmail"].ToString()).userName;

            return PartialView();

       }


    }
}