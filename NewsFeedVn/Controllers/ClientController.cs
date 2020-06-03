using NewsFeedVn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Controllers
{
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ClientTest()
        {
            return View();
        }
        public ActionResult Post(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var listArticle = db.Articles.Where( a => a.CategoryID == article.CategoryID).Take(12);
            var comment = db.Comments.Where(c => c.ArticleID == article.Id);

            var viewModel = new ViewPostModel() {
                Article = article,
                Comments = comment.ToList(),
                ListArticle = listArticle.ToList()
            };
            return View("~/Views/Client/Post.cshtml", viewModel);
        }
        //public ActionResult CategoriesDetail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}
    }
}