using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emdo.Models;
namespace emdo.Controllers
{
    public class newsController : Controller
    {
        
        // GET: news
        BlogContext db = new BlogContext();
        public ActionResult add()
        {
            if (Session["userid"] == null) return RedirectToAction("login","user");
            SelectList st = new SelectList(db.catalogs.ToList(),"id","name");
            ViewBag.cat = st;
            return View();
        }
        [HttpPost]
        public ActionResult add(news n,HttpPostedFileBase img)
        {
            img.SaveAs(Server.MapPath($"~/attach/newsphoto/{ img.FileName}"));

            n.photo = $"/attach/newsphoto/{ img.FileName}";
            n.user_id =(int) Session["userid"];
            n.date = DateTime.Now;
            db.news.Add(n);
            db.SaveChanges();
            return RedirectToAction("mynews");
        }
        public ActionResult mynews()
        {
            int userid = (int)Session["userid"];
            List<news> ns = db.news.Where(n => n.user_id == userid).ToList();
            return View(ns);
        }
        public ActionResult details(int id)
        {
            news s= db.news.Where(n => n.user_id == id).FirstOrDefault();
            return View(s);
        }
        public ActionResult allnews()
        {
            return View(db.news.ToList());
        }
        public ActionResult delete(int id)
        {
            news s = db.news.Where(n => n.user_id == id).FirstOrDefault();
            db.news.Remove(s);
            db.SaveChanges();
            return RedirectToAction("mynews");
        }
        public ActionResult edit(int id)
        {
            user s = db.users.Where(n => n.Id == id).FirstOrDefault();
            return View(s);
        }
        [HttpPost]
        public ActionResult edit(news s)
        {
            news ns = db.news.Where(n => n.user_id == n.id).SingleOrDefault();
            ns.title = s.title;
            ns.bref = s.bref;
            ns.desc = s.desc;
           


            db.SaveChanges();


            return RedirectToAction("mynews");
        }
    }
}