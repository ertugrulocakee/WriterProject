using BL.Concrete;
using BL.Validation;
using DAL.EF;
using Entities.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WriterProject.Models;
using PagedList;
using PagedList.Mvc;

namespace WriterProject.Controllers
{

    [Authorize(Roles = "1")]
    public class WriterController : Controller
    {
        // GET: Writer

        WriterManager writerManager = new WriterManager(new EFWriterDAL());
        HeadingManager headingManager = new HeadingManager(new EFHeadingDAL());


   
        public ActionResult Index(int page = 1)
        {
            var writers = writerManager.TGetList().Where(x=>x.WriterStatus == true).ToPagedList(page,6);
            return View(writers);
        }



        public ActionResult DeleteWriter(int id)
        {

            var value = writerManager.TGetByID(id);

            value.WriterStatus = false;

            writerManager.TUpdate(value);

            return RedirectToAction("Index");

        }


        public ActionResult WriterHeadings(int id,int page=1)
        {

            var headings = headingManager.GetListByWriter(id).Where(x=>x.HeadingStatus == true).ToPagedList(page,5);

            return View(headings);
             
        }



    }
}