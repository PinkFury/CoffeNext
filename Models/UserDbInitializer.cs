using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CoffeeNext.Models
{
    public class UserDbInitializer: DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Users.Add(new User { User_Name = "admin", Email = "admin", Password = "330412", PhoneNumber = "0", Points = 2281488 }); 
            base.Seed(db);
        }
    }
}