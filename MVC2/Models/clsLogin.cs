﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC2.Models
{
    public class clsLogin
    {
        [Required]
        public string Account { get; set; }
      
        public string pw { get; set; }
         
    }
}