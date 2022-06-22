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
using WriterProject.Models;
using System.IO;
using System.Web.Security;

namespace WriterProject.Controllers
{
    [Authorize(Roles="2")]
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel

        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());
        AdminManager adminManager = new AdminManager(new EFAdminDAL());

        [HttpGet]
        public ActionResult WriterProfile()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            string writerMail = Session["WriterMail"].ToString();

            var writer  = writerManager.GetWriterByMail(writerMail);

            WriterViewModel writerViewModel = new WriterViewModel();

            writerViewModel.id = writer.WriterID;
            writerViewModel.WriterName = writer.WriterName;
            writerViewModel.WriterSurName = writer.WriterSurName;
            writerViewModel.WriterMail = writer.WriterMail;
            writerViewModel.WriterPassword = writer.WriterPassword;
            writerViewModel.WriterDescription = writer.WriterDescription;
            writerViewModel.ImagePath = writer.WriterImage;



            return View(writerViewModel);

        }

        [HttpPost]
        public ActionResult WriterProfile(WriterViewModel writerViewModel)
        {

            Writer writer = writerManager.TGetByID(writerViewModel.id);


            writer.WriterName = writerViewModel.WriterName;
            writer.WriterSurName = writerViewModel.WriterSurName;
            writer.WriterMail = writerViewModel.WriterMail;
            writer.WriterPassword = writerViewModel.WriterPassword;
            writer.WriterDescription = writerViewModel.WriterDescription;


            WriterValidation writerValidator = new WriterValidation();
            ValidationResult validationResult = writerValidator.Validate(writer);


            if (validationResult.IsValid)
            {

                var values = writerManager.TGetList().Where(m => (m.WriterMail == writer.WriterMail) && m.WriterID != writer.WriterID && m.WriterStatus == true).ToList();

                if (values.Any())
                {

                    TempData["message"] = "Lutfen e-posta adresiniz benzersiz olsun! Bu e-posta kullaniliyor!";

                    return RedirectToAction("WriterProfile", "WriterPanel");

                }

                var valuesAdmin = adminManager.TGetList().Where(m => m.email == writer.WriterMail).ToList();

                if (valuesAdmin.Any())
                {

                    TempData["message"] = "Lutfen e-posta adresiniz benzersiz olsun! Bu e-posta kullaniliyor!";

                    return RedirectToAction("WriterProfile", "WriterPanel");

                }

                if (writerViewModel.Image != null)
                {

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

                        return RedirectToAction("WriterProfile", "WriterPanel");

                    }


                    writerViewModel.ImagePath = "/Images/WriterImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/WriterImages/"), fileName);
                    writerViewModel.Image.SaveAs(fileName);


                    writer.WriterImage = writerViewModel.ImagePath;

                }

                writer.WriterStatus = true;
                writerManager.TUpdate(writer);
                TempData["message"] = "Yazar basarili bir sekilde guncellendi!";
                return RedirectToAction("WriterProfile", "WriterPanel");

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

                var headings = headingManager.GetListByHeadingName(heading.HeadingName);

                if (headings.Any())
                {

                    TempData["message"] = "Boyle bir baslik daha once acilmis!";

                    return RedirectToAction("NewHeading", "WriterPanel");

                }
                else
                {
                    string writerMail = Session["WriterMail"].ToString();
                    heading.HeadingDate = DateTime.Now;
                    heading.HeadingStatus = true;
                    heading.WriterID = writerManager.GetWriterByMail(writerMail).WriterID;
                    headingManager.TAdd(heading);
                    return RedirectToAction("MyHeading");

                }

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

                return RedirectToAction("EditHeading", "WriterPanel");

            }

            HeadingValidator validation = new HeadingValidator();
            ValidationResult validationResult = validation.Validate(heading);

            if (validationResult.IsValid)
            {

                var headings = headingManager.GetListByHeadingName(heading.HeadingName).Where(m => m.HeadingID != heading.HeadingID);


                if (headings.Any())
                {

                    TempData["message"] =  "Boyle bir baslik daha once acilmis!";

                    return RedirectToAction("EditHeading", "WriterPanel");

                }
                else
                {

                    var value = headingManager.TGetByID(heading.HeadingID);

                    value.Category = heading.Category;
                    value.HeadingDate = DateTime.Now;
                    value.HeadingName = heading.HeadingName;


                    headingManager.TUpdate(value);
                    return RedirectToAction("MyHeading");

                }

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


        public ActionResult AllHeadings(int page=1)
        {

            var headings = headingManager.TGetList().Where(m=>m.HeadingStatus == true).ToPagedList(page,10);


            return View(headings);

        }


        public PartialViewResult WriterPicture()
        {

            ViewBag.picture = writerManager.GetWriterByMail(Session["WriterMail"].ToString()).WriterImage;
            ViewBag.nameSurname = writerManager.GetWriterByMail(Session["WriterMail"].ToString()).WriterName + " " + writerManager.GetWriterByMail(Session["WriterMail"].ToString()).WriterSurName;

            return PartialView();

        }


        public ActionResult DeleteWriter(int id)
        {

            var value = writerManager.TGetByID(id);

            value.WriterStatus = false;

            writerManager.TUpdate(value);

            FormsAuthentication.SignOut();

            Session.Abandon();

            return RedirectToAction("HomePage", "Home");

        }


        public PartialViewResult MessageBox()
        {

            MessageManager messageManager = new MessageManager(new EFMessageDAL());

            string writerMail = Session["WriterMail"].ToString();

            var messages = messageManager.GetReceiveBox(writerMail).Where(m=>m.MessageStatus==true).OrderByDescending(m=>m.Date).Take(5).ToList();

            ViewBag.messageCount = messageManager.GetReceiveBox(writerMail).Where(m => m.MessageStatus == true).Count();

            return PartialView(messages);

        }

        public PartialViewResult NotificationBox()
        {
            var headings = headingManager.TGetList().Where(m => m.HeadingStatus == true).OrderByDescending(m => m.HeadingDate).Take(5).ToList();

            ViewBag.headingCount = headingManager.TGetList().Where(m => m.HeadingStatus == true).Count();

            return PartialView(headings);

        }

    }
}