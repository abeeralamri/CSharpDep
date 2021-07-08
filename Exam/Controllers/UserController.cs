using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exam.Models;


namespace WeddingPlanner.Controllers
{
    public class UserController : Controller
    {
        private ExamContext DbContext;

        public UserController(ExamContext context)
        {
            DbContext = context;
        } 

        [HttpGet("")]

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost("Register")]

        public IActionResult Register(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(DbContext.Users.Any(e=>e.UserName == newUser.UserName))
                {
                    ModelState.AddModelError("UserName", "Username is already exist!");
                    return View("Index");
                }
                if(newUser.UserName.Length > 15)
                {
                    ModelState.AddModelError("UserName", "Username must be less than 15 character!");
                    return View("Index");
                }

                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    DbContext.Users.Add(newUser);
                    DbContext.SaveChanges();
                    HttpContext.Session.SetInt32("uid", newUser.UserId);
                    return RedirectToAction("Dashboard", "Hobby");
                }

               
            }
             return View("Index");
        }


        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
           if (ModelState.IsValid)
            {
                User user = DbContext.Users.FirstOrDefault( u => u.UserName == login.LoginUserName);
                if (user == null)
                {
                    ModelState.AddModelError("LoginUserName", "Invalid Login ");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<Login> Hasher = new PasswordHasher<Login>();

                    var result = Hasher.VerifyHashedPassword(login, user.Password, login.LoginPassword);

                    if (result == 0)
                    {
                        ModelState.AddModelError("LoginUserName", "Invalid Login ");
                        return View("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("uid", user.UserId);
                        return RedirectToAction("Dashboard", "Hobby");
                    }
                }
                
            }
            else 
            {
                return View("Index");
            }
        }        

        

       

        

    }
}