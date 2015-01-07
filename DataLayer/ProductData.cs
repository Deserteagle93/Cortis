using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class ProductData:ConnectionClass
    {
       public ProductData() : base() { }
       public ProductData(CommonLayer.MarketEntities Entities) : base(Entities) { }

       public void editProduct(CommonLayer.Product product)
       {
           CommonLayer.Product prod = this.Entities.Products.SingleOrDefault(p=>p.Productid==product.Productid);
           prod.ProductDescription = product.ProductDescription;
           prod.Price = product.Price;
           prod.ProductName = product.ProductName;
           prod.Stock = product.Stock;
           this.Entities.SaveChanges();
       }

        public IQueryable<CommonLayer.Product> getProducts()
        {
            return this.Entities.Products;
        }
        public CommonLayer.Product getProduct(Guid id)
        {
            return this.Entities.Products.FirstOrDefault(p=>p.Productid==id);
        }

        public IQueryable<CommonLayer.Product> getProducts(string name)
        {
            return this.Entities.Products.Where(p => p.User.Username==name);
        }

        public void addProduct(CommonLayer.Product product)
        {
            this.Entities.Products.AddObject(product);
            this.Entities.SaveChanges();
        }

        public void deleteProduct(CommonLayer.Product Product)
        {
            this.Entities.Products.Attach(Product);
            this.Entities.Products.DeleteObject(Product);
            this.Entities.SaveChanges();
        }


    }
}
