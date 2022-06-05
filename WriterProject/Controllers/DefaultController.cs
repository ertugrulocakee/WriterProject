using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        // GET: Default

        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());
        ContentManager contentManager = new ContentManager(new EFContentDAL());

        public ActionResult Headings()
        {
            var headingList = headingManager.TGetList();

            return View(headingList);

        }


        public PartialViewResult Index(int id = 0)
        {

            var contentList = contentManager.GetListByID(id);    

            return PartialView(contentList);
        }
    }
}