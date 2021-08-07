using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeazyBattles.Shared
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(16, ErrorMessage = "Password must be shorter than 16 characters.")]
        public string Username { get; set; }
        public string Bio { get; set; }
        [Required, StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The passwords entered do not match.")]
        public string ConfirmPassword { get; set; }
        public int StartUnitId { get; set; } = 1;
        //[Range(0, 1000, ErrorMessage = "Users must agree to Terms and Conditions.")]
        //public int Bananas { get; set; } = 100;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Users must agree to Terms and Conditions.")]
        public bool IsConfirmed { get; set; } = false;

    }
}
