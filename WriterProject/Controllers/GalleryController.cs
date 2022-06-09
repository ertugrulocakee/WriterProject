using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    [Authorize(Roles = "1")]
    public class GalleryController : Controller
    {
        // GET: Gallery

        WriterManager writerManager = new WriterManager(new EFWriterDAL());

        public ActionResult Index()
        {
            var values = writerManager.TGetList().Where(m => m.WriterStatus == true).ToList();

            return View(values);
        }
    }
}