using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkMangement;

namespace WorkMangement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly WorkDL workDL;
        private readonly PhaseDL phaseDL;
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(SignInManager<ApplicationUser> signinManager, UserManager<ApplicationUser> userManager, ApplicationDBContext context)
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            workDL = new WorkDL(context);
            phaseDL = new PhaseDL(context);
        }

        /// <summary>
        /// Hàm đến trang chủ hiển thị thông tin nhân viên
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            // Kiểm tra đã đăng nhập hệ thống hay chưa
            if (!signinManager.IsSignedIn(User))
            {
                // Yêu cầu đăng nhập
                return RedirectToAction("Login", "Account");
            }
            else
            {
                // Lấy danh sách user
                var model = userManager.Users.ToList();
                return View(model);
            }
        }

        /// <summary>
        /// Đến trang hiển thị danh sách nhân viên
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public IActionResult ListUsers()
        {
            var model = userManager.Users.ToList();
            return View("Index", model);
        }

        /// <summary>
        /// Đến trang danh sách công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public IActionResult ListWorks()
        {
            var model = workDL.GetAllWorks();
            return View(model);
        }
    }
}
