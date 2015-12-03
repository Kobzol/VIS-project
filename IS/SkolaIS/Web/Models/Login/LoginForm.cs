using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Login
{
    public class LoginForm
    {
        [Required]
        [StringLength(50, MinimumLength=1)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; } 
    }
}