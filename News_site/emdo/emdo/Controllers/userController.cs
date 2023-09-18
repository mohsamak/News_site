using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emdo.Models;

namespace emdo.Controllers
{
    public class userController : Controller
    {
        // GET: user
        BlogContext db =new BlogContext();
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(user s ,HttpPostedFileBase img )
        {
            user d = db.users.Where(n => n.email == s.email).FirstOrDefault();
            if (d != null)
            {
                ViewBag.status = "Email aleardy exist!!!!";
                return View();

            }
            img.SaveAs(Server.MapPath("~/attach/" + img.FileName));
            s.photo = img.FileName;
            
            if (ModelState.IsValid)
            {
                db.users.Add(s);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user s)
        {
            user d = db.users.Where(n => n.username == s.username && n.password == s.password).FirstOrDefault();
            if(d!=null)
            {
                //login
                Session.Add("userid", d.Id);
                return RedirectToAction("profile");
            }
            else
            {
                //not login
                ViewBag.status   = "incrrect user or password";
                return View();
            }

        }
        public ActionResult profile()
        {
            if(Session["userid"]==null)
            {
                return RedirectToAction("login");
            }
            int id = (int)Session["userid"];
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(s);
        }
        public ActionResult logout()
        {
            Session["userid"] = null;
            return RedirectToAction("login");

        }
        public ActionResult edit(int id)
        {
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public ActionResult edit(user u)
        {
            user us = db.users.Where(n => n.Id == u.Id).SingleOrDefault();
            us.username = u.username;
            us.age = u.age;
            us.email = u.email;
            us.address = u.address;
            us.confirm_password = us.password;


            db.SaveChanges();


            return RedirectToAction("profile");
        }
    }
}