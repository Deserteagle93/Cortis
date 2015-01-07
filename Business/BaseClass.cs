using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class BaseClass
    {
        private CommonLayer.MarketEntities _Entities;

        public CommonLayer.MarketEntities Entities
        {
            get { return _Entities; }
            set { _Entities = value; }
        }

        public BaseClass()
        {
            DataLayer.ConnectionClass cl = new DataLayer.ConnectionClass();
            this.Entities = cl.Entities;
        }
    }
}
