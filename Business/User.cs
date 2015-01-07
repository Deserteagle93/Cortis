using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace Business
{
    public class User:BaseClass
    {
         public User() : base() { }

         public static CommonLayer.User getuser(string username)
         {
             DataLayer.UserData ud = new DataLayer.UserData();
             CommonLayer.User theUser = ud.getUser(username);
             return theUser;
         }

         public static bool checkLogin(string user, string password)
         {
             DataLayer.UserData ud = new DataLayer.UserData();
             CommonLayer.User theUser = ud.getUser(user);
             if (theUser != null)
             {
                 if (theUser.Password == password)
                 {
                     return true;
                 }
             }
             return false;
         }
         public static bool userIsSeller(string user)
         {
             DataLayer.UserData ud = new DataLayer.UserData();
             CommonLayer.User theUser = ud.getUser(user);
             if (theUser != null)
             {
                 if (theUser.Roleid == 2)
                 {
                     return true;
                 }
             }
             return false;
         }

        public static bool ChangePassword(string user, string oldp, string newp)
        {
            DataLayer.UserData ud = new DataLayer.UserData();
            CommonLayer.User theUser = ud.getUser(user);
            if (theUser.Password == oldp)
            {
                ud.ChangePassword(user, newp);
                return true;
            }
            else return false;
        }



        public static int Register(string userName, string password, string email, bool seller, bool buyer)
        {
            DataLayer.UserData ud = new DataLayer.UserData();
            if (seller != buyer)
            {
                if (ud.getUser(userName) == null)
                {
                    if (ud.checkEmail(email))
                    {
                        CommonLayer.User usr = new CommonLayer.User();
                        usr.Email = email;
                        usr.Username = userName;
                        usr.Password = password;
                        if (seller)
                        {
                            usr.Roleid = 2;
                        }
                        else if (buyer)
                        {
                            usr.Roleid = 1;
                        } usr.Userid = Guid.NewGuid();
                        ud.RegisterUser(usr);
                        return 3;
                    }
                    else { return 2; }
                }
                else { return 1; }
            }
            else { return 0; }
        }



        public static IQueryable<CommonLayer.User> getUsers()
        {
            return new DataLayer.UserData().getUsers();
        }
    }
}
