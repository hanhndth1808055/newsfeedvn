using NewsFeedVn.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
            var LastestArticle = getLastestArticle();
            ViewHomeModel viewHomeModel = new ViewHomeModel() { 
                SlideArticle = db.Articles.Where(p => (int) p.Status == 2 && p.Img != null && p.Img != " ").Take(3).ToList(),
                SimilarPostSlide = db.Articles.Where(p => (int)p.Status == 2 && p.Img != null && p.Img != " ").Take(9).ToList(),
                Categories = db.Categories.ToList(),
                TopStories = db.Articles.Where(p => (int)p.Status == 2 && p.Img != null && p.Img != " ").OrderByDescending(a => a.Count).Take(12).ToList(),
                DontMiss = db.Articles.Where(p => (int)p.Status == 2 && p.Img != null && p.Img != " ").Take(8).ToList(),
                WhatsTrending = db.Articles.Where(p => (int)p.Status == 2 && p.Img != null && p.Img != " ").Take(7).ToList(),
                LastestArticle = LastestArticle,
            };
            ViewBag.MenuHeaderActive = "Home";
            return View(viewHomeModel);
        }
        
        public ActionResult Search(string keyword)
        {
            ViewCategoryModel viewCategories;
            var LastestArticle = getLastestArticle();
            var articles = db.Articles.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                articles = articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").Where(a => a.Content.Contains(keyword) || a.Title.Contains(keyword) || a.Description.Contains(keyword));
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = articles.ToList(),
                    SameArticles = db.Articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").OrderBy(a => a.CreatedAt).OrderByDescending(a => a.Count).Take(12).ToList(),
                    LastestArticle = LastestArticle
                };
                ViewBag.CategoryId = 0;
            }
            else
            {
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    SameArticles = db.Articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").OrderBy(a => a.CreatedAt).OrderByDescending(a => a.Count).Take(12).ToList(),
                    LastestArticle = LastestArticle
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

            var LastestArticle = getLastestArticle();
            var check = true;
            foreach (var item in LastestArticle)
            {
                if (item.Id == article.Id)
                {
                    check = false;
                    break;
                }
            }
            if (check == true)
            {
                LastestArticle.Insert(0, article);
                setLastestArticle(LastestArticle);
            }

            var listArticle = db.Articles.Where(p => (int)p.Status == 2 && p.Img != null && p.Img != " ").OrderByDescending(a => a.Count).Take(12);
            var comment = db.Comments.Where(c => c.ArticleID == article.Id);

            var viewModel = new ViewPostModel()
            {
                Article = article,
                Comments = comment.ToList(),
                ListArticle = listArticle.ToList(),
                LastestArticle = LastestArticle
            };

            article.Count++;
            db.Entry(article).State = EntityState.Modified;
            db.SaveChanges();

            return View("~/Views/Client/Post.cshtml", viewModel);
        }
        public List<Article> getLastestArticle()
        {
            List<Article> LastestArticle = null;
            if (Session["LastestArticle"] != null)
            {
                LastestArticle = Session["LastestArticle"] as List<Article>;
            }
            if (Session["LastestArticle"] == null)
            {
                LastestArticle = new List<Article>();
            }
            return LastestArticle;
        }

        public void setLastestArticle(List<Article> LastestArticle)
        {
            Session["LastestArticle"] = LastestArticle;
        }
        public ActionResult CategoriesIndex(int? id, int? index)
        {
            ViewCategoryModel viewCategories;
            List<Article> articles = null;
            var LastestArticle = getLastestArticle();
            if (id != null && id != 0)
            {
                var Categories = db.Categories.ToList();
                for (int i = 0; i < Categories.Count; i++)
                {
                    if (Categories[i].Id == id)
                    {
                        var tmp = Categories[i];
                        Categories[i] = Categories[0];
                        Categories[0] = tmp;
                    }
                }
                articles = db.Articles.Where(a => a.CategoryID == id && (int)a.Status == 2 && a.Img != null && a.Img != " ").Take(25).ToList();
                viewCategories = new ViewCategoryModel()
                {
                    Categories = Categories,
                    Articles = articles,
                    SameArticles = db.Articles.Where(a => a.CategoryID == id && (int)a.Status == 2 && a.Img != null && a.Img != " ").OrderBy(a => a.CreatedAt).OrderByDescending(a => a.Count).Take(12).ToList(),
                    LastestArticle = LastestArticle
                };
                ViewBag.CategoryId = id;
            }
            else
            {
                articles = db.Articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").Take(25).ToList();
                viewCategories = new ViewCategoryModel()
                {
                    Categories = db.Categories.ToList(),
                    Articles = articles,
                    SameArticles = db.Articles.Where( a => (int)a.Status == 2 && a.Img != null && a.Img != " ").OrderBy(a => a.CreatedAt).OrderByDescending(a => a.Count).Take(12).ToList(),
                    LastestArticle = LastestArticle
                };
                ViewBag.CategoryId = 0;
            }
            if (index != null)
            {
                articles = db.Articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").Take(index.Value * 25).ToList();
                viewCategories.Articles = articles;
                ViewBag.Index = index;
            }
            else
            {
                ViewBag.Index = 2;
            }
            ViewBag.MenuHeaderActive = "Categories";
            return View("~/Views/Client/Category.cshtml", viewCategories);
        }
        public string AjaxCategoriesIndex(int index)
        {
            var articleMore = db.Articles.Where(a => (int)a.Status == 2 && a.Img != null && a.Img != " ").Take(index * 25).ToList();
            var stringJson = JsonConvert.SerializeObject(articleMore);
            return stringJson;
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

        // GET: Categories/Create
        public ActionResult SendQuestion()
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
                return RedirectToAction("SendQuestion");
            }

            return View("SendQuestion", questionAndAnswer);
        }
    }
}