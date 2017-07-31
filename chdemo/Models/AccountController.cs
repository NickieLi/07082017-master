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
        [HttpGet]
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(member mModel)
        {
            using(er_modelEntities2 db = new er_modelEntities2())
            {
                var usr = db.member.Single(u => u.Account == mModel.Account && u.Password == mModel.Password);
                if (mModel != null)
                {
                    Session["ID"] = usr.ID.ToString();
                    Session["Name"] = usr.Account.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","Name or Password is wrong.");
                }
            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}