﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeazyBattles.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter a valid username.")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
