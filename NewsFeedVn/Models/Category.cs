﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsFeedVn.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please input this field")]
        [MaxLength(20, ErrorMessage = "The input excesses the maximum length of 20 characters")]
        public string Name { get; set; }
    }
}