using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kalender_BL;
using PagedList;
using PagedList.Mvc;

namespace praktik.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int? page)
        {
            return View(Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 5));
        }

        // GET: /Home/CreateActivity

        public ActionResult CreateActivity()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult CreateActivity(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
