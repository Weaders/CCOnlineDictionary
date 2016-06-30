﻿using Microsoft.AspNet.Identity.Owin;
using OnlineDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineDiary.Controllers
{    
    public class DiaryController : Controller
    {
        private ApplicationUserManager _userManager = null;
        private ApplicationDbContext context = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [Authorize]
        public async Task<ActionResult> Schelude() {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (await UserManager.IsInRoleAsync(user.Id, "teacher"))
            {
                var viewModel = new TeacherScheduleViewModel();
                viewModel.Teacher = user;
                return View("TeacherSchedule", viewModel);
            }
            else if (await UserManager.IsInRoleAsync(user.Id, "parent"))
            {
                var viewModel = new ParentUserScheduleViewModel();
                viewModel.Parent = user;
                return View("ParentSchedule", viewModel);
            }
            else if (await UserManager.IsInRoleAsync(user.Id, "children")) {
                var viewModel = new UserScheduleViewModel();
                viewModel.User = user;
                return View("ChildrenSchedule", viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Marks(int lessonId = 2) {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (await UserManager.IsInRoleAsync(user.Id, "teacher"))
            {
                //Get All Lessons of teacher
                var lessons = context.Lessons.All(l => l.TeacherId == user.Id);
            }
            else if(await UserManager.IsInRoleAsync(user.Id, "parent"))
            {

            }
            return RedirectToAction("Index", "Home");
        }
    }
}
/*
 * Parent 
 * Оценки для рёбёнка
 * Lessons\Days 1
 * Lessons1     3
 * --------
 * Children <-> Parent
 * --------
 * Teacher
 * Оценки за Физику для 5-Б
 * Children\Day 1
 * Петя         3
 * ------
 */