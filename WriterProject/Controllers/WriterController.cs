using BL.Concrete;
using BL.Validation;
using DAL.EF;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WriterProject.Models;

namespace WriterProject.Controllers
{
    public class WriterController : Controller
    {
        // GET: Writer

        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Index()
        {
            var writers = writerManager.TGetList().Where(x=>x.WriterStatus == true).ToList();
            return View(writers);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }

            return View();

        }


        [HttpPost]
        public ActionResult AddWriter(WriterViewModel writerViewModel)
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

                var values = writerManager.TGetList().Where(m=>m.WriterMail == writer.WriterMail || m.WriterPassword == writer.WriterPassword).ToList();
                
                if(values.Any())
                {

                    TempData["message"] = "Lutfen e-posta ve sifre benzersiz degerlere sahip olsun!";

                    return RedirectToAction("AddWriter", "Writer");

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

                    return RedirectToAction("AddWriter", "Writer");

                }


                writerViewModel.ImagePath = "/Images/WriterImages/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/WriterImages/"), fileName);
                writerViewModel.Image.SaveAs(fileName);


                writer.WriterImage = writerViewModel.ImagePath;
                writer.WriterStatus = true;
                writerManager.TAdd(writer);
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


        public ActionResult DeleteWriter(int id)
        {

            var value = writerManager.TGetByID(id);

            value.WriterStatus = false;

            writerManager.TUpdate(value);

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult UpdateWriter(int id)
        {


            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];

            }


            var writer = writerManager.TGetByID(id);

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
        public ActionResult UpdateWriter(WriterViewModel writerViewModel)
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

                var values = writerManager.TGetList().Where(m => (m.WriterMail == writer.WriterMail || m.WriterPassword == writer.WriterPassword) && m.WriterID != writer.WriterID && m.WriterStatus == true).ToList();

                if (values.Any())
                {

                    TempData["message"] = "Lutfen e-posta ve sifre benzersiz degerlere sahip olsun!";

                    return RedirectToAction("AddWriter", "Writer");

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

                        return RedirectToAction("AddWriter", "Writer");

                    }


                    writerViewModel.ImagePath = "/Images/WriterImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/WriterImages/"), fileName);
                    writerViewModel.Image.SaveAs(fileName);


                    writer.WriterImage = writerViewModel.ImagePath;

                }
            
                writer.WriterStatus = true;
                writerManager.TUpdate(writer);
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



    }
}