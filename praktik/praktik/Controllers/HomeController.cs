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

        public ActionResult Index(int? page, string filter, string sorting, string print)
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;

            ViewBag.SortingParaDate = string.IsNullOrEmpty(sorting) ? "Datum desc" : "";
            ViewBag.SortingParaArr = sorting == "Arrangor" ? "Arrangor desc" : "Arrangor";
            ViewBag.SortingParaOrt = sorting == "Ort" ? "Ort desc" : "Ort";


            var activity = Activity.getAllActivities().AsQueryable();

           
                if (filter != null && filter != "showAll")
                    activity = activity.Where(x => x.Activitet.StartsWith(filter.Trim()) || filter == null);
                else
                    activity = Activity.getAllActivities().AsQueryable();

                switch (sorting)
                {
                    case "Arrangor desc":
                        activity = activity.OrderByDescending(x => x.Arrangor);
                        break;
                    case "Arrangor":
                        activity = activity.OrderBy(x => x.Arrangor);
                        break;
                    case "Ort desc":
                        activity = activity.OrderByDescending(x => x.Ort);
                        break;
                    case "Ort":
                        activity = activity.OrderBy(x => x.Ort);
                        break;
                    case "Datum desc":
                        activity = activity.OrderByDescending(x => x.Datum);
                        break;
                    default:
                        activity = activity.OrderBy(x => x.Datum);
                        break;
                }

                if (print != "print")
                {
                    ViewBag.printStatus = null;
                    return View(activity.ToPagedList(page ?? 1, 10));
                }
                else
                {
                    ViewBag.printStatus = "print";
                    return View(activity.ToPagedList(page ?? 1, 1000));
                }


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
                    //string url = b.Hemsida;
                    //if(!url.Contains("http://"))
                    //{
                    //    url = "http://" + url;
                    //    b.Hemsida = url;
                    //}
                    Activity.insertActivity(b);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception er)
            {
                throw er;
                //System.Diagnostics.Debug.WriteLine(er);
                //List<string> aL = Activity.getActivityName();
                //ViewBag.ActivityList = aL;
                //return View();
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


        public ActionResult EditActivity(int ID, int? page)
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;
            if (Session["user"] != null)
            {
                UsersMod user = (UsersMod)Session["user"];
                if (user.Type == "Admin")
                    return View(Activity.getActivityByName(ID));
                else
                {
                    Session["user"] = null;
                    return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                }
            }
            else
            {
                Session["user"] = null;
                return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
            }
        }

        [HttpPost]
        public ActionResult EditActivity(int? page)
        {
            List<string> aL = Activity.getActivityName();
            ViewBag.ActivityList = aL;
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
                            string url = b.Hemsida;
                            if (!url.Contains("http://"))
                            {
                                url = "http://" + url;
                                b.Hemsida = url;
                            }
                            b.saveActivity();
                            return RedirectToAction("Index");
                        }
                        else
                            return View();
                    }
                    else
                    {
                        Session["user"] = null;
                        return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                    }
                }
                else
                {
                    Session["user"] = null;
                    return View("Index", Activity.getAllActivities().ToList().ToPagedList(page ?? 1, 10));
                }
            }
            catch (Exception er)
            {
                System.Diagnostics.Debug.WriteLine(er);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(int ID, int? page)
        {
            if (Session["user"] != null)
            {
                UsersMod user = (UsersMod)Session["user"];
                if (user.Type == "Admin")
                {
                    Activity b = Activity.getActivityByName(ID);
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
