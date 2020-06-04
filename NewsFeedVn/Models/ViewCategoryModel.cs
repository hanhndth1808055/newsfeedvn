using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class ViewCategoryModel
    {
        public List<Category> Categories { get; set; }
        public List<Article> Articles { get; set; }
        public List<Article> SameArticles { get; set; }
    }
}