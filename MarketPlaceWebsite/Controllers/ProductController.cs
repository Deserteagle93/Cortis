using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPlaceWebsite.Controllers
{
    [HandleError]
    public class ProductsController : Controller
    {
        public ActionResult Products()
        {
            return View(new Business.Product().getProducts());
        }
        public ActionResult SellerProduct()
        {
            try
            {
                return View(new Business.Product().getProducts(HttpContext.User.Identity.Name));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CommonLayer.Product model)
        {
            model.Userid = Business.User.getuser(HttpContext.User.Identity.Name).Userid;
            Business.Product.addProduct(model);

            //Obeserver start
            Observer.Email email = new Observer.Email();
            IQueryable<CommonLayer.User> users = Business.User.getUsers();
            foreach (CommonLayer.User u in users)
            {
                if (u.Roleid == 1)
                {
                    Observer.IEmailList buyer = new Observer.Buyer(u.Email);
                    email.subscribe(buyer);
                }
                else if (u.Roleid == 2)
                {
                    Observer.IEmailList seller = new Observer.Seller(u.Email);
                    email.subscribe(seller);
                }
            }

            email.Notify(model.ProductName,model.ProductDescription);
            //OBeserver end
            return RedirectToAction("SellerProduct");
        }
        public ActionResult Delete(object id)
        {
            Business.Product.deleteProduct(Guid.Parse(id.ToString()));
            return RedirectToAction("SellerProduct");
        }
        public ActionResult Edit(object id)
        {
            return View(Business.Product.getProduct(Guid.Parse(id.ToString())));
        }
        [HttpPost]
        public ActionResult Edit(CommonLayer.Product product)
        {
            Business.Product.editProduct(product);
            return RedirectToAction("SellerProduct");
        }
        public ActionResult Details(object id)
        {            
            return View(Business.Product.getProduct(Guid.Parse(id.ToString())));
        }
    }
}
