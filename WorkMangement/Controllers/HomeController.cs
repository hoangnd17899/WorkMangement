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

        /// <summary>
        /// Hàm đến trang thêm mới công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public IActionResult AddWork()
        {
            return View();
        }

        /// <summary>
        /// Hàm thêm mới công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddWork(WorkCreateView model)
        {
            if (ModelState.IsValid)
            {
                var rs = workDL.AddWork(model);
                if (rs)
                {
                    return RedirectToAction("ListWorks");
                }
                ViewData["ErrorMessage"] = "Thêm mới dự án không thành công";
            }

            return View();
        }

        /// <summary>
        /// Đến trang chỉnh sửa thông tin công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="projectId">id của dự án</param>
        /// <returns></returns>
        public IActionResult EditWork(Guid workId)
        {
            var model = workDL.GetWorkById(workId);
            return View(model);
        }

        /// <summary>
        /// Hàm sửa thông tin công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditWork(WorkEditView pro)
        {
            if (ModelState.IsValid)
            {
                var rs = workDL.EditWork(pro);
                if (rs)
                {
                    return RedirectToAction("ListWorks");
                }

                ModelState.AddModelError("", "Sửa thông tin dự án không thành công");
            }
            return View();
        }

        /// <summary>
        /// Hàm xóa dữ liệu công việc (bao gồm cả các pha)
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IActionResult DeleteWork(Guid workId)
        {
            var rs = workDL.DeleteWork(workId);
            if (rs == false)
            {
                ModelState.AddModelError("", "Xóa dự án không thành công");
            }
            return RedirectToAction("ListWorks");
        }

        /// <summary>
        /// Đến trang thêm pha vào công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public IActionResult AddPhase(Guid workId)
        {
            var model = new PhaseCreateView
            {
                WorkId = workId
            };
            return View(model);
        }

        /// <summary>
        /// Hàm thêm pha vào công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="model">công việc</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddPhase(PhaseCreateView model)
        {
            var rs = phaseDL.AddPhase(model);
            if (rs)
            {
                return RedirectToAction("EditWork", new { workId = model.WorkId });
            }

            ModelState.AddModelError("", "Thêm mới công việc không thành công");
            return View(new { workId = model.EmployeeId });
        }

        /// <summary>
        /// Hàm xóa pha trong công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public IActionResult DeletePhase(Guid phaseId)
        {
            var rs = phaseDL.DeletePhase(phaseId);
            if (rs == Guid.Empty)
            {
                ModelState.AddModelError("", "Xóa công việc không thành công");
            }

            return RedirectToAction("EditWork", new { workId = rs });
        }

        /// <summary>
        /// Hàm đến trang sửa pha trong công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditPhase(Guid phaseId)
        {
            var model = phaseDL.GetPhaseById(phaseId);
            return View(model);
        }

        /// <summary>
        /// Hàm sửa thông tin pha
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditPhase(PhaseEditView work)
        {
            if (ModelState.IsValid)
            {
                var model = phaseDL.EditPhase(work);
                if (model)
                {
                    return RedirectToAction("EditWork", new { workId = work.WorkId });
                }

                ModelState.AddModelError("", "Thay đổi thông tin công việc không thành công");
            }

            return View();
        }
    }
}
