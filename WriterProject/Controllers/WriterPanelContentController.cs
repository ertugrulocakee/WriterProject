using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent

        ContentManager contentManager = new ContentManager(new EFContentDAL());

        public ActionResult MyContent()
        {

            var contentValues = contentManager.GetListByWriter().Where(m => m.ContentStatus == true).ToList();
            return View(contentValues);

        }
    }
}