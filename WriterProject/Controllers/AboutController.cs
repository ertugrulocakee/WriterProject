using BL.Concrete;
using DAL.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: About

        AboutManager aboutManager = new AboutManager(new EFAboutDAL());
        public ActionResult Index()
        {
            var about = aboutManager.TGetList();
            return View(about);

        }

        [HttpGet]
        public ActionResult AddAbout()
        {

            return View();

        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {

            aboutManager.TAdd(about);

            return RedirectToAction("Index");

        }

        public PartialViewResult AboutPartial()
        {

            return PartialView();

        }



    }
}