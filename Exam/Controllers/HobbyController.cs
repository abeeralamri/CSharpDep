using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Exam.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeddingPlanner.Controllers
{
    public class HobbyController : Controller
    {
        private ExamContext DbContext;

        public HobbyController(ExamContext context)
        {
            DbContext = context;
        } 

        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("uid") != null)
            {
                int id = (int)HttpContext.Session.GetInt32("uid");
                List<Hobby> ListHobby = DbContext.Hobbies
                .Include(h=>h.Creator)
                .Include(h=>h.UserHobby)
                .ThenInclude(u=>u.User)
                .ToList();
                ViewBag.UserID = id;
            
            return View("Dashboard", ListHobby);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet("AddHobby")]
        public ViewResult AddHobby()
        {
            return View();
        }

        [HttpPost("new")]
          public IActionResult Create(Hobby newHobby)
        {
           if(ModelState.IsValid)
            {
                  if(DbContext.Hobbies.Any(h=>h.Name == newHobby.Name))
                {
                    ModelState.AddModelError("Name", "This hobby is already exist!");
                    return View("AddHobby");
                }
                
                    newHobby.UserId = (int) HttpContext.Session.GetInt32("uid");
                    DbContext.Hobbies.Add(newHobby);
                    DbContext.SaveChanges();
                    return Redirect("/Dashboard");
                
            }
            else
            {
                return View("AddHobby");
            }
        }

        [HttpGet("Details/{HobbyID}")]
        public IActionResult Details(int HobbyID)
        {
            int id = (int)HttpContext.Session.GetInt32("uid");
            ViewBag.UserID = id;
            Hobby ThisHobby = DbContext.Hobbies
            .Include(w=>w.UserHobby)
            .ThenInclude(u=>u.User)
            .FirstOrDefault(w=>w.HobbyId == HobbyID);
            return View("Details", ThisHobby);
        }



        [HttpGet("enthusiast/{HobbyID}")]
        public IActionResult Enthusiast(int HobbyID)
        {
            UserHobby ThisEnthusiast = new UserHobby();
            ThisEnthusiast.UserId = (int)HttpContext.Session.GetInt32("uid");
            ThisEnthusiast.HobbyId = HobbyID;
            DbContext.UserHobbies.Add(ThisEnthusiast);
            DbContext.SaveChanges();
            return RedirectToAction("Dashboard");

        }
        
        

        [HttpGet("edit/{HobbyID}")]
        public IActionResult Edit(int HobbyID)
        {
            Hobby thisHobby = DbContext.Hobbies.FirstOrDefault(h=>h.HobbyId == HobbyID);
            return View(thisHobby);
        }

        [HttpPost("editH/{HobbyID}")]
        public IActionResult EditProcess(int HobbyID, Hobby EditH)
        {
            Hobby thisHobby = DbContext.Hobbies.FirstOrDefault(h=>h.HobbyId == HobbyID);

            if(ModelState.IsValid)
            {
                 if(DbContext.Hobbies.Any(h=>h.Name == EditH.Name))
                {
                    ModelState.AddModelError("Name", "This hobby is already exist!");
                    return View("Edit");
                }
                
                    thisHobby.Name = EditH.Name;
                    thisHobby.Description = EditH.Description;
                    thisHobby.UpdatedAt = DateTime.Now;
                    DbContext.SaveChanges();
                    return Redirect($"/Details/{thisHobby.HobbyId}");
                
            }
            else
            {
                return View("Edit");
            }

        }
        [HttpGet("logout")]
        public RedirectToActionResult Lougout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "User");
        }
    }
}