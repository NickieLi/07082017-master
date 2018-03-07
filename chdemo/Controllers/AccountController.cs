using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using chdemo.Models;
using System.Data.Entity.Infrastructure;

namespace chdemo.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }  //seems like have no use of this page when HomeController combines in this project.


        public ActionResult Register()
        {
            member user = new member();
            return View(user);
        }

        [HttpPost]
        public ActionResult Register(member MemberModel)
        {
            using (er_modelEntities2 dbm = new er_modelEntities2())
            {
                if (dbm.member.Any(x => x.Account == MemberModel.Account))
                {
                    ViewBag.DuplicateMessage = "Account already exist";
                    return View("Register", MemberModel);
                }
                dbm.member.Add(MemberModel);
                dbm.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("Register", new member());
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(member user)
        {
            using (er_modelEntities2 db = new er_modelEntities2())
            {
                var usr = db.member.Where(u => u.Account == user.Account && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserID"] = usr.Account.ToString();
                    Session["Username"] = usr.Account.ToString();  //have no use in authorization

                    return RedirectToAction("LoggedIn");

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
            }
            return View();
        }


        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Logout()
        {

            Session.Clear();
            // Session["UserID"] = null; 
            //this is not good cause session is still existing.
            return RedirectToAction("Index", "Home");

        }
    }
}