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

        ImageFileManager imageFileManager = new ImageFileManager(new EFImageFileDAL());

        public ActionResult Index()
        {
            var values = imageFileManager.TGetList();

            return View(values);
        }
    }
}