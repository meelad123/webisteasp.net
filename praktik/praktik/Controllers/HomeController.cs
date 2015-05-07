using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kalender_BL;
using PagedList;
using PagedList.Mvc;
using System.Net;
using Newtonsoft.Json;

namespace praktik.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int? page, string filter)
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;
            if (filter != null && filter != "showAll")
                return View(Activity.getAllActivities().Where(x => x.Activitet.StartsWith(filter.Trim()) || filter == null).ToList().ToPagedList(page ?? 1, 10));
            else
                return View(Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
        }

        // GET: /Home/CreateActivity

        public ActionResult CreateActivity()
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult CreateActivity(FormCollection collection)
        {
            try
            {
                Activity b = new Activity();
                TryUpdateModel(b);
                var response = collection["g-recaptcha-response"];
                const string secret = "6LenJQYTAAAAAI8VvqiJsSSGyHYY3aBVjsm8-C5x";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<Captcha>(reply);

                if (!captchaResponse.Success || !ModelState.IsValid)
                {
                    List<string> aL = Activity.getActivityName();
                    ViewBag.ActivityList = aL;
                    ViewBag.Message = "Fel recaptcha!";
                    return View();
                }
                else
                {
                    Activity.insertActivity(b);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                List<string> aL = Activity.getActivityName();
                ViewBag.ActivityList = aL;
                return View();
            }
        }

        public ActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Admin(FormCollection collection, int? page)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            UsersMod u = new UsersMod();
            TryUpdateModel(u);
            if (ModelState.IsValid)
            {
                u.UserName = collection["username"];
                u.password = collection["password"];
                UsersMod nUser = u.checkUser(u);
                Activity b = new Activity();
                TryUpdateModel(b);

                if (nUser.UserName != null)
                {
                    if (nUser.password == crypto.Compute(u.password, nUser.passwordSalt))
                    {
                        List<string> aL = Activity.getActivityName();
                        ViewBag.ActivityList = aL;
                        Session["user"] = nUser;
                        return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                    }
                    else
                    {
                        ViewBag.WrongInfo = "Wrong password!";
                        return View("Admin");
                    }
                }
                else
                {
                    ViewBag.WrongInfo = "Failed to log in. Check your info!";
                    return View("Admin");
                }
            }
            else
            {
                ViewBag.WrongInfo = "Failed to log in. Check your info!";
                return View("Admin");
            }
        }

        public ActionResult Logout(int? page)
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;
            Session["user"] = null;
            return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
        }


        public ActionResult EditActivity(string aNamn, DateTime aDatum, int? page)
        {
            if (Session["user"] != null)
            {
                UsersMod user = (UsersMod)Session["user"];
                if (user.Type == "Admin")
                    return View(Activity.getActivityByName(aNamn, aDatum));
                else
                {
                    List<string> aL = Activity.getActivityName();
                    ViewBag.ActivityList = aL;
                    Session["user"] = null;
                    return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                List<string> aL = Activity.getActivityName();
                ViewBag.ActivityList = aL;
                Session["user"] = null;
                return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
            }
        }

        [HttpPost]
        public ActionResult EditActivity(int? page)
        {
            try
            {
                if (Session["user"] != null)
                {
                    UsersMod user = (UsersMod)Session["user"];
                    if (user.Type == "Admin")
                    {
                        Activity b = new Activity();
                        TryUpdateModel(b);
                        if (ModelState.IsValid)
                        {
                            b.saveActivity();
                            return RedirectToAction("Index");
                        }
                        else
                            return View();
                    }
                    else
                    {
                        List<string> aL = Activity.getActivityName();
                        ViewBag.ActivityList = aL;
                        Session["user"] = null;
                        return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    List<string> aL = Activity.getActivityName();
                    ViewBag.ActivityList = aL;
                    Session["user"] = null;
                    return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(string aNamn, DateTime aDatum, int? page)
        {
            if (Session["user"] != null)
            {
                UsersMod user = (UsersMod)Session["user"];
                if (user.Type == "Admin")
                {
                    Activity b = Activity.getActivityByName(aNamn, aDatum);
                    b.removeActivity();
                    return RedirectToAction("Index");
                }
                else
                {
                    List<string> aL = Activity.getActivityName();
                    ViewBag.ActivityList = aL;
                    Session["user"] = null;
                    return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                List<string> aL = Activity.getActivityName();
                ViewBag.ActivityList = aL;
                Session["user"] = null;
                return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
            }
        }

        private void createUser()
        {
            var crypto = new SimpleCrypto.PBKDF2();
            UsersMod addUser = new UsersMod();
            string pass = "12344321";
            addUser.userID = "1";
            addUser.UserName = "Admin";
            addUser.Type = "Admin";
            addUser.password = crypto.Compute(pass);
            addUser.passwordSalt = crypto.Salt;

            UsersMod.createUser(addUser);
        }
    }
}
