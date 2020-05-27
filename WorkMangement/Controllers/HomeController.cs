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
                if (User.IsInRole("admin"))
                {
                    var model = userManager.Users.ToList();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ListWorks");
                }
            }
        }

        /// <summary>
        /// Đến trang hiển thị danh sách nhân viên
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
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

        /// <summary>
        /// Hàm đến trang xem danh sách công việc
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ViewWork(Guid workId)
        {
            var model = workDL.GetWorkById(workId);
            return View(model);
        }

        [HttpPost]
        public IActionResult ViewWork(WorkEditView model)
        {
            List<UserPhasesViewModel> lst = new List<UserPhasesViewModel>();
            if (model.WorkPhases != null)
            {
                foreach (Phase phase in model.WorkPhases)
                {
                    if (phase.EmployeeId.ToString() == userManager.GetUserId(User))
                    {
                        UserPhasesViewModel userPhasesViewModel = new UserPhasesViewModel
                        {
                            PhaseId = phase.PhaseId,
                            IsFinish = phase.IsFinish,
                        };
                        lst.Add(userPhasesViewModel);
                    }
                }

                var rs = phaseDL.UpdatePhasesStatus(lst);
                if (!rs)
                {
                    TempData["Error"] = "Có lỗi xảy ra !";
                }
            }
            
            return RedirectToAction("ViewWork", new { workId = model.WorkId });
        }
    }
}
