using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeNext.Models
{
    public class User
    {
        public int Id { get; set; }
        public string User_Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int Points { get; set; }
    }
}