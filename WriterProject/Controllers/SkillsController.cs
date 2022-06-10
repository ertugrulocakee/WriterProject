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
    [Authorize(Roles="2")]
    public class SkillsController : Controller
    {
        // GET: Skills

        SkillManager skillManager = new SkillManager(new EFSkillDAL());
        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Index()
        {

            var values = skillManager.TGetList().Where(m=>m.SkillStatus == true).ToList();

            return View(values);
        }

      
        [HttpGet]
        public ActionResult AddSkill()
        {

            if (TempData["message"] != null)
            {

                ViewBag.Message = TempData["message"];  

            }


            Writers();
            return View();

        }

        [HttpPost]  
        public ActionResult AddSkill(Skill skill)
        {
            try
            {

                var writer = writerManager.TGetByID(skill.Writer.WriterID);
                skill.Writer = writer;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);

                TempData["message"] = "Secilecek yazar yok! Lutfen once yazar olusturun!";

                return RedirectToAction("AddSkill", "Skills");

            }

            SkillValidator validation = new SkillValidator();
            ValidationResult validationResult = validation.Validate(skill);

            if (validationResult.IsValid)
            {

                skill.SkillStatus = true;
                skillManager.TAdd(skill);
                return RedirectToAction("Index");

            }
            else
            {

                foreach (var item in validationResult.Errors)
                {

                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }


            }

            Writers();
            return View();


        }


        public ActionResult DeleteSkill(int id)
        {

            var value = skillManager.TGetByID(id);


            value.SkillStatus = false;


            skillManager.TUpdate(value);


            return RedirectToAction("Index");

        }

        protected void Writers()
        {
            List<SelectListItem> writersList = (from x in writerManager.TGetList().Where(m => m.WriterStatus == true)
                                                select new SelectListItem
                                                {


                                                    Text = x.WriterName + " " + x.WriterSurName,
                                                    Value = x.WriterID.ToString()


                                                }).ToList();

            ViewBag.writers = writersList;


        }


    }
}