using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class ViewHomeModel
    {
        public List<Article> SlideArticle { get; set; }
        public List<Article> SimilarPostSlide { get; set; }
        public List<Category> Categories { get; set; }
        public List<Article> TopStories { get; set; }
        public List<Article> DontMiss { get; set; }
        public List<Article> WhatsTrending { get; set; }
        public List<Article> LastestArticle { get; set; }
    }
}