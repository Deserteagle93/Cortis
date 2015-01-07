using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPlaceWebsite.Observer
{
    public class Email
    {
        private IList<IEmailList> subscribers = new List<IEmailList>();

        public void subscribe(IEmailList e)
        {
            subscribers.Add(e);
        }
        public void Unsubscribe(IEmailList e)
        {
            subscribers.Remove(e);
        }
        public void Notify(string Product,string Desc)
        {
            foreach (IEmailList e in subscribers)
            {
                e.notify(Product, Desc);
            }
        }
    }
}