using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class Source
    {
        public int Id { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Path { get; set; }
        //Category
        [ForeignKey("Category")]
        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }
        public virtual Category Category { get; set; }
        [Display(Name = "Link Selector")]
        [Required]
        public string Link_selector { get; set; }
        [Display(Name = "Title Selector")]
        [Required]
        public string Title_selector { get; set; }
        [Display(Name = "Content Selector")]
        [Required]
        public string Content_selector { get; set; }
        [Display(Name = "Image Selector")]
        [Required]
        public string Img_selector { get; set; }
        public SourceStatus Status { get; set; }
        public enum SourceStatus
        {
            DEACTIVE = 0, ACTIVE = 1, DELETE = 2

        }
        public String toString()
        {
            return "sourdeID: " + Id +
                    ", Domain: " + Domain +
                    ", Path: " + Path +
                    ", CategoryID: " + CategoryID +
                    ", Link_selector: " + Link_selector +
                    ", Title_selector: " + Title_selector +
                    ", Content_selector: " + Content_selector +
                    ", Img_selector: " + Img_selector +
                    ", Status: " + Status.ToString();
        }
    }
}