using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace eCommerceSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly CommerceContext _context;
        private readonly IHttpContextAccessor _accessor;

        public AccountController(CommerceContext context,
                IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Member m)
        {
            if (ModelState.IsValid)
            {
                //add to db
                MemberDb.AddMember(m, _context);
                SessionHelper.LogUserIn(_accessor, m.MemberId);

                //redirect to index page
                return RedirectToAction("Index", "Home");
            }

            //returning view, with error messages
            return View(m);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //check if member exists
                Member member = (from m in _context.Members
                                          where m.Email == model.Email &&
                                                 m.Password == model.Password
                                          select m).SingleOrDefault();
                if(member != null)
                {
                    SessionHelper.LogUserIn(_accessor, member.MemberId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //tell user credentials do not match a record
                    ModelState.AddModelError("", "Credentials not found");
                    return View(model);
                }
            }
            //return view with errors
            return View(model);
        }

        public IActionResult LogOut()
        {
            //Destroy Session (Log the user out)
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}