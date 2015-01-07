using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace MarketPlaceWebsite.Observer
{
    public class Seller:IEmailList
    {
        public string email = "";
        public Seller(string email)
        {
            this.email = email;
        }
        public void notify(string item,string desc)
        {            
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("JoeBroMan@gmail.com");
                mail.To.Add(email);
                mail.Subject = "New Product added!";
                mail.Body = "Hello there from MarketPlace! A new item has been added to our website! Make sure that if you have the same item for sale you have the most competative price! Item:" + item + " Come check it out it is described as:" + desc;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("JoeBroMan@gmail.com", "abc123456");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
    }
}