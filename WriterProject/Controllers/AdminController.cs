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

    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
        // GET: Admin

        AdminManager adminManager = new AdminManager(new EFAdminDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        [HttpGet]
        public ActionResult AddAdmin()
        {           
            return View();
        }

        [HttpPost]  
        public ActionResult AddAdmin(Admin admin)
        {


            AdminValidator adminValidator = new AdminValidator();
            ValidationResult result = adminValidator.Validate(admin);


            if (result.IsValid)
            {

                var admins = adminManager.TGetList().Where(m=>m.email == admin.email || m.userName == admin.userName).ToList();

                if (admins.Any())
                {

                    ViewBag.Message = "E-posta adresiniz veya kullanıcı adınız bir adminle ortaktır! Lutfen farkli degerler deneyin!";

                    return View();

                }

                var writers = writerManager.TGetList().Where(m=>m.WriterStatus == true).Where(m => m.WriterMail == admin.email).ToList();

                if (writers.Any())
                {

                    ViewBag.Message = "E-posta adresiniz  bir yazarla ortaktır! Lutfen farkli degerler deneyin!";

                    return View();

                }


                admin.role = "1";

                adminManager.TAdd(admin);

                ViewBag.Message = "Yeni admin basariyla eklendi/tanimlandi!";

                return View();  

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



        [HttpGet]
        public ActionResult EditAdmin()
        {

            var admin = adminManager.GetAdmin(Session["AdminEmail"].ToString());

            return View(admin);
            
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {

            AdminValidator adminValidator = new AdminValidator();
            ValidationResult result = adminValidator.Validate(admin);


            if (result.IsValid)
            {

                var admins = adminManager.TGetList().Where(m => (m.email == admin.email || m.userName == admin.userName) && m.id != admin.id).ToList();

                if (admins.Any())
                {

                    ViewBag.Message = "E-posta adresiniz veya kullanıcı adınız bir adminle ortaktır! Lutfen farkli degerler deneyin!";

                    return View();

                }

                var writers = writerManager.TGetList().Where(m=>m.WriterStatus==true).Where(m => m.WriterMail == admin.email).ToList();

                if (writers.Any())
                {

                    ViewBag.Message = "E-posta adresiniz  bir yazarla ortaktır! Lutfen farkli degerler deneyin!";

                    return View();

                }

                var value = adminManager.TGetByID(admin.id);

                value.email = admin.email;
                value.password = admin.password;
                value.userName = admin.userName;

                adminManager.TUpdate(value);

                ViewBag.Message = "Admin basariyla guncellendi!";

                return View();

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