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

            string writerMail = Session["WriterMail"].ToString();

            var skills = skillManager.GetWriterWithSkills(writerMail);

            return View(skills);

        }

      
        [HttpGet]
        public ActionResult AddSkill()
        {

     
            return View();

        }

        [HttpPost]  
        public ActionResult AddSkill(Skill skill)
        {
      

            SkillValidator validation = new SkillValidator();
            ValidationResult validationResult = validation.Validate(skill);

            if (validationResult.IsValid)
            {

                string writerMail = Session["WriterMail"].ToString();


                skill.WriterID = writerManager.GetWriterByMail(writerMail).WriterID;
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

       
            return View();

        }


        public ActionResult DeleteSkill(int id)
        {

            var value = skillManager.TGetByID(id);


            value.SkillStatus = false;


            skillManager.TUpdate(value);


            return RedirectToAction("Index");

        }

      
    }
}