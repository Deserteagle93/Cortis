using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MarketPlaceWebsite.Models;

namespace MarketPlaceWebsite.Controllers
{
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Business.User.checkLogin(model.UserName,model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email,model.Seller,model.Buyer);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (Business.User.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        public ActionResult Roles()
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    return View(new Business.Roles().getRoles());
                }
            }
            return RedirectToAction("LogOn");
        }

        public ActionResult Create()
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    return View();
                }
            }
            return RedirectToAction("LogOn");
        }

        [HttpPost]
        public ActionResult Create(CommonLayer.Role model)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    List<CommonLayer.Role> roles = new Business.Roles().getRoles().ToList();
                    int max = 0;
                    foreach (CommonLayer.Role r in roles)
                    {
                        if (max <= r.Roleid)
                        {
                            max = r.Roleid;
                        }
                    } model.Roleid = max+1;
                    new Business.Roles().AddRole(model);
                    return RedirectToAction("Roles");
                }
            }
            return RedirectToAction("LogOn");
        }

        public ActionResult Delete(object id)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    Business.Roles.DeleteRole(Convert.ToInt32(id.ToString()));
                }
            }
            return RedirectToAction("Roles");
        }

        public ActionResult Edit(object id)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    return View(new Business.Roles().getRole(Convert.ToInt32(id.ToString())));
                }
            } return RedirectToAction("LogOn"); 
        }
        [HttpPost]
        public ActionResult Edit(CommonLayer.Role model)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    if (model.Role1 != null)
                    {
                        if (model.Role1 != "")
                        {
                            new Business.Roles().EditRole(model);
                            return RedirectToAction("Roles");
                        }
                    }
                }
            }
            return RedirectToAction("LogOn");
        }

        public ActionResult Details(object id)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                if (Business.User.userIsAdmin(HttpContext.User.Identity.Name))
                {
                    return View(new Business.Roles().getRole(Convert.ToInt32(id)));
                }
            }
            return RedirectToAction("LogOn");
        }
                

    }
}
