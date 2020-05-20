using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsFeedVn.Models;

namespace NewsFeedVn.Controllers
{
    public class Sources1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sources1
        public ActionResult Index()
        {
            //var sources = db.Sources.Include(s => s.Category);
            List<Source> sources = db.Sources.ToList();
            for (int i = 0; i < sources.Count; i++)
            {
                if (sources[i].Status.Equals(1))
                {
                    Debug.WriteLine(sources[i]);
                }
            };

            return View(sources.ToList());
        }

        // GET: Sources1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // GET: Sources1/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Sources1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Domain,Path,CategoryID,Link_selector,Content_selector,Img_selector,Status")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Sources.Add(source);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", source.CategoryID);
            return View(source);
        }

        // GET: Sources1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", source.CategoryID);
            return View(source);
        }

        // POST: Sources1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Domain,Path,CategoryID,Link_selector,Content_selector,Img_selector,Status")] Source source)
        {
            if (ModelState.IsValid)
            {
                db.Entry(source).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", source.CategoryID);
            return View(source);
        }

        // GET: Sources1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return HttpNotFound();
            }
            return View(source);
        }

        // POST: Sources1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Source source = db.Sources.Find(id);
            db.Sources.Remove(source);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
