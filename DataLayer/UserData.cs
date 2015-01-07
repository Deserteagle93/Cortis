using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Objects;


namespace DataLayer
{
    public class UserData:ConnectionClass
    {
         public UserData() : base() { }
         public UserData(CommonLayer.MarketEntities Entities) : base(Entities) { }

         public CommonLayer.User getUser(string username)
         {
             try
             {
                 return this.Entities.Users.FirstOrDefault(u => u.Username == username);
             }
             catch (InvalidOperationException ex)
             {
                 return null;
             }
         }

         public void ChangePassword(string user, string newp)
         {
             CommonLayer.User userr = this.Entities.Users.SingleOrDefault(u => u.Username == user);
             userr.Password = newp;
             this.Entities.SaveChanges();
         }

         public bool checkEmail(string email)
         {
             try
             {
                 CommonLayer.User user = this.Entities.Users.FirstOrDefault(p => p.Email == email);
                 if (user == null)
                 {
                     return true;
                 }
                 else return false;
             }
             catch (InvalidOperationException ex)
             {
                 return false;
             }
         }

         public void RegisterUser(CommonLayer.User usr)
         {
             this.Entities.Users.AddObject(usr);
             this.Entities.SaveChanges();
         }

         public IQueryable<CommonLayer.User> getUsers()
         {
             return this.Entities.Users;
         }
    }
}
