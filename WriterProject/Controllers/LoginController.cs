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
using WriterProject.Models;

namespace WriterProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login

        AdminManager adminManager = new AdminManager(new EFAdminDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

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
                    FormsAuthentication.SetAuthCookie(admin.userName, false);
                    Session["AdminUserName"] = admin.userName;
                    return RedirectToAction("Index", "Category");

                }
                else
                {

                    ViewBag.Message = "Boyle bir admin yoktur!";

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


        [HttpGet]
        public ActionResult WriterLogin()
        {

            return View();

        }

        [HttpPost]
        public ActionResult WriterLogin(WriterLoginViewModel writerLoginViewModel)
        {

            var values = writerManager.TGetList().Where(m => m.WriterMail == writerLoginViewModel.WriterMail && m.WriterPassword == writerLoginViewModel.WriterPassword).FirstOrDefault();


            if(ModelState.IsValid)
            {

                if (values != null)
                {
                    FormsAuthentication.SetAuthCookie(writerLoginViewModel.WriterMail, false);
                    Session["WriterMail"] = writerLoginViewModel.WriterMail;
                    return RedirectToAction("MyContent", "WriterPanelContent");

                }
                else
                {

                    ViewBag.Message = "Boyle bir yazar yoktur!";

                    return View();

                }

            }
            else
            {



                return View();


            }

        }


        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("HomePage", "Home");
        }

      
    }
}