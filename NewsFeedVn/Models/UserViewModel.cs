using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public Boolean Status { get; set; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}