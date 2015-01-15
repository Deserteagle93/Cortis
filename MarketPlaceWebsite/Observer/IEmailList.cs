using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPlaceWebsite.Observer
{
    public interface IEmailList
    {
        void addemail(string a);
        void notify(string a, string b);
    }
}