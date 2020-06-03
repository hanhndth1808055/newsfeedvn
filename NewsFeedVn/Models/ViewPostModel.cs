using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class ViewPostModel
    {
        public Article Article { get; set; }
        public List<Article> ListArticle { get; set; }
        public List<Comment> Comments { get; set; }
    }
}