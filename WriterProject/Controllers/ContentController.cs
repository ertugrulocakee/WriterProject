using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WriterProject.Controllers
{
    [Authorize(Roles = "1")]
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager contentManager = new ContentManager(new EFContentDAL());


        public ActionResult GetAllContents(string p,int page=1)
        {

            var values = contentManager.GetListByInputValue(p);

            return View(values.ToPagedList(page, 10));    
       
        }

        
        public ActionResult ContentByHeader(int id,int page = 1)
        {
            var contentValues = contentManager.GetListByID(id).Where(m=>m.ContentStatus == true).ToPagedList(page,5);
            return View(contentValues);

        }


    }
}