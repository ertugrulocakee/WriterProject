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


        public ActionResult GetAllContents(int? page,string content = "")
        {

            ViewBag.Content = content;

            var values = contentManager.GetListByInputValue(content);

            int pageSize = 10;
            int pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View(values.ToPagedList(pageIndex, pageSize));    
       
        }

        
        public ActionResult ContentByHeader(int id,int page = 1)
        {
            var contentValues = contentManager.GetListByID(id).Where(m=>m.ContentStatus == true).ToPagedList(page,5);
            return View(contentValues);

        }


    }
}