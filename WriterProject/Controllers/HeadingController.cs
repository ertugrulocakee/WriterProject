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
    public class HeadingController : Controller
    {
        // GET: Heading

        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());

        public ActionResult HeadingReport()
        {
            var values = headingManager.TGetList().Where(m => m.HeadingStatus == true).ToList();

            return View(values);

        }


    }
}