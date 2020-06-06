using NewsFeedVn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Controllers
{
    public class QuestionAndAnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionAndAnswers
        public ActionResult Index()
        {
            return View(db.QuestionAndAnswers.ToList());
        }

        // GET: QuestionAndAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAndAnswer questionAndAnswer = db.QuestionAndAnswers.Find(id);
            if (questionAndAnswer == null)
            {
                return HttpNotFound();
            }
            return View(questionAndAnswer);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UserID,CategoryID,Title,Content")] QuestionAndAnswer questionAndAnswer)
        {
            if (ModelState.IsValid)
            {
                db.QuestionAndAnswers.Add(questionAndAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionAndAnswer);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAndAnswer questionAndAnswer = db.QuestionAndAnswers.Find(id);
            if (questionAndAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Username");
            return View(questionAndAnswer);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserID,CategoryID,Title,Content")] QuestionAndAnswer questionAndAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionAndAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionAndAnswer);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAndAnswer questionAndAnswer = db.QuestionAndAnswers.Find(id);
            if (questionAndAnswer == null)
            {
                return HttpNotFound();
            }
            return View(questionAndAnswer);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionAndAnswer questionAndAnswer = db.QuestionAndAnswers.Find(id);
            db.QuestionAndAnswers.Remove(questionAndAnswer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Categories/DeleteArrayConfirmed
        [HttpPost]
        public ActionResult DeleteArrayConfirmed(string[] idArray)
        {
            try
            {
                foreach (var id in idArray)
                {
                    var currentId = Int32.Parse(id);
                    QuestionAndAnswer questionAndAnswer = db.QuestionAndAnswers.Find(currentId);
                    db.QuestionAndAnswers.Remove(questionAndAnswer);
                }
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return null;
            }
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
