using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeNext.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
    public class AuthModel
    {
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}