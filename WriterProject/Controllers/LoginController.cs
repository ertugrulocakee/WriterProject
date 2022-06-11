using BL.Concrete;
using BL.Validation;
using DAL.EF;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Index(AdminLoginViewModel adminLoginViewModel)
        {

            var value = adminManager.TGetList().Where(m => m.email == adminLoginViewModel.email && m.password == adminLoginViewModel.password).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (value != null)
                {
                    FormsAuthentication.SetAuthCookie(value.email, false);
                    Session["AdminEmail"] = value.email;
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


                return View();


            }

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


            if (ModelState.IsValid)
            {

                if (values != null)
                {
                    FormsAuthentication.SetAuthCookie(writerLoginViewModel.WriterMail, false);
                    Session["WriterMail"] = writerLoginViewModel.WriterMail;
                    return RedirectToAction("WriterProfile", "WriterPanel");

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


        [HttpGet]
        public ActionResult Register()
        {


            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            return View();

        }

        [HttpPost]
        public ActionResult Register(WriterViewModel writerViewModel)
        {

            Writer writer = new Writer();


            writer.WriterName = writerViewModel.WriterName;
            writer.WriterSurName = writerViewModel.WriterSurName;
            writer.WriterMail = writerViewModel.WriterMail;
            writer.WriterPassword = writerViewModel.WriterPassword;
            writer.WriterDescription = writerViewModel.WriterDescription;


            WriterValidation writerValidator = new WriterValidation();
            ValidationResult validationResult = writerValidator.Validate(writer);


            if (validationResult.IsValid)
            {

                var values = writerManager.TGetList().Where(m => m.WriterStatus == true).Where(m => m.WriterMail == writer.WriterMail).ToList();

                if (values.Any())
                {

                    TempData["message"] = "Lutfen e-posta adresiniz benzersiz olsun! Bu e-posta kullaniliyor!";

                    return RedirectToAction("Register", "Login");

                }

                var valuesAdmin = adminManager.TGetList().Where(m => m.email == writer.WriterMail).ToList();

                if (valuesAdmin.Any())
                {

                    TempData["message"] = "Lutfen e-posta adresiniz benzersiz olsun! Bu e-posta kullaniliyor!";

                    return RedirectToAction("Register", "Login");

                }


                string[] validFileTypes = { "gif", "jpg", "png" };
                bool isValidType = false;


                string fileName = Path.GetFileName(writerViewModel.Image.FileName);
                string extension = Path.GetExtension(writerViewModel.Image.FileName);

                for (int i = 0; i < validFileTypes.Length; i++)
                {
                    if (extension == "." + validFileTypes[i])
                    {
                        isValidType = true;
                        break;
                    }
                }

                if (!isValidType)
                {
                    TempData["message"] = "Lutfen png,jpg ve gif dosyasi yukleyin!";

                    return RedirectToAction("Register", "Login");

                }


                writerViewModel.ImagePath = "/Images/WriterImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/WriterImages/"), fileName);
                writerViewModel.Image.SaveAs(fileName);


                writer.WriterImage = writerViewModel.ImagePath;
                writer.WriterStatus = true;
                writer.role = "2";
                writerManager.TAdd(writer);
                return RedirectToAction("WriterLogin");

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


    }
}