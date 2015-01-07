using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ConnectionClass
    {
        private CommonLayer.MarketEntities _Entities;

        public CommonLayer.MarketEntities Entities
        {
            get { return _Entities; }
            set { _Entities = value; }
        }

        public ConnectionClass()
        {
            Entities = new CommonLayer.MarketEntities();
        }

        public ConnectionClass(CommonLayer.MarketEntities lEntities)
        {
            this.Entities = lEntities;
        }
    }
}
