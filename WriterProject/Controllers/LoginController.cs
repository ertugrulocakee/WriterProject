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
    public class LoginController : Controller
    {
        // GET: Login

        AdminManager adminManager = new AdminManager(new EFAdminDAL());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]  
        public ActionResult Index(Admin admin)
        {

            var values = adminManager.TGetList().Where(m=>m.userName == admin.userName && m.password == admin.password).FirstOrDefault();


            AdminValidator validator = new AdminValidator();
            ValidationResult result = validator.Validate(admin);

            if (result.IsValid)
            {

                if (values != null)
                {

                    return RedirectToAction("Index", "Category");

                }
                else
                {

                    ViewBag.Message = "Boyle bir kullanici yoktur!";

                    return View();

                }

            }
            else
            {

                foreach(var item in result.Errors)
                {

                    ModelState.AddModelError(item.PropertyName,item.ErrorCode);


                }

                

            }


             return View(); 


        }

    }
}