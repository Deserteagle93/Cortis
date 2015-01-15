using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace Business
{
    public class Product:BaseClass
    {
        public Product() : base() { }

        public IQueryable<CommonLayer.Product> getProducts()
        {
            return new DataLayer.ProductData().getProducts();
        }

        public IQueryable<CommonLayer.Product> getProducts(string Username)
        {
            return new DataLayer.ProductData().getProducts(Username);
        }

        public static void addProduct(CommonLayer.Product model)
        {
            model.Productid = Guid.NewGuid();
            new DataLayer.ProductData().addProduct(model);
        }

        public static void deleteProduct(Guid guid)
        {
            DataLayer.ProductData pd = new DataLayer.ProductData();
            pd.deleteProduct(pd.getProduct(guid));
        }

        public static CommonLayer.Product getProduct(Guid id)
        {
            DataLayer.ProductData pd = new DataLayer.ProductData();
            return pd.getProduct(id);
        }

        public static void editProduct(CommonLayer.Product product)
        {
            new DataLayer.ProductData().editProduct(product);
        }
    }
}
