﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Admin.Models
{
    public class Account { 
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }
        public string NameOfStaff { get; set; }
    }
}