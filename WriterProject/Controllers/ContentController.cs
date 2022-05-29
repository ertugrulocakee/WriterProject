﻿using BL.Concrete;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriterProject.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager contentManager = new ContentManager(new EFContentDAL());

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ContentByHeader(int id)
        {
            var contentValues = contentManager.GetListByID(id).Where(m=>m.ContentStatus == true).ToList();
            return View(contentValues);

        }


    }
}