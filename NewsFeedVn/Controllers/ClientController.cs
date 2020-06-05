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
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Client
        public ActionResult Home()
        {
            ViewHomeModel viewHomeModel = new ViewHomeModel() { 
                SlideArticle = db.Articles.Where(p => (int) p.Status == 2).Take(3).ToList(),
                SimilarPostSlide = db.Articles.Where(p => (int)p.Status == 2).Take(9).ToList(),
                Categories = db.Categories.ToList(),
                TopStories = db.Articles.Where(p => (int)p.Status == 2).Take(12).ToList(),
                DontMiss = db.Articles.Where(p => (int)p.Status == 2).Take(8).ToList(),
                WhatsTrending = db.Articles.Where(p => (int)p.Status == 2).Take(7).ToList(),
                LastestArticle = db.Articles.Where(p => (int)p.Status == 2).Take(11).ToList(),
            };
            return View(viewHomeModel);
        }
        public ActionResult Search(string keyword)
        {
            ViewCategoryModel viewCategories;
            var articles = db.Articles.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                articles = articles.Where(p => (int)p.Status == 2).Where(a => a.Content.Contains(keyword) || a.Title.Contains(keyword) || a.Description.Contains(keyword));
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = articles.ToList(),
                    SameArticles = db.Articles.OrderBy(a => a.CreatedAt).Take(12).ToList()
                };
            }
            else
            {
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = articles.ToList(),
                    SameArticles = db.Articles.OrderBy(a => a.CreatedAt).Take(12).ToList()
                };
            }
            return View(viewCategories);
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
            var listArticle = db.Articles.Where(p => (int)p.Status == 2).Where( a => a.CategoryID == article.CategoryID).Take(12);
            var comment = db.Comments.Where(c => c.ArticleID == article.Id);

            var viewModel = new ViewPostModel() {
                Article = article,
                Comments = comment.ToList(),
                ListArticle = listArticle.ToList()
            };
            return View("~/Views/Client/Post.cshtml", viewModel);
        }
        public ActionResult CategoriesIndex(int? id)
        {
            ViewCategoryModel viewCategories;
            if (id != null)
            {
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = db.Articles.Where(a => a.CategoryID == id).Take(25).ToList(),
                    SameArticles = db.Articles.Where(a => a.CategoryID == id).OrderBy(a => a.CreatedAt).Take(12).ToList()
                };
            }
            else
            {
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = db.Articles.Where(a => a.CategoryID == id).Take(25).ToList(),
                    SameArticles = db.Articles.Where(a => a.CategoryID == id).OrderBy(a => a.CreatedAt).Take(12).ToList()
                };
            }
            
            return View("~/Views/Client/Category.cshtml", viewCategories);
        }
        public JsonResult AjaxCategoriesIndex(int index)
        {
            var articleMore = db.Articles.AsQueryable();
            articleMore = articleMore.Where(p => (int)p.Status == 2).OrderBy(a => a.CreatedAt).Skip((index - 1) * 25).Take(25);
            return Json(articleMore, JsonRequestBehavior.AllowGet);
        }
        // public ActionResult CategoriesDetail(int? id)
        // {
        //     if (id == null)
        //     {
        //         return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //     }
        //     Category category = db.Categories.Find(id);
        //     if (category == null)
        //     {
        //         return HttpNotFound();
        //     }
        //     return View(category);
        // }
    }
}