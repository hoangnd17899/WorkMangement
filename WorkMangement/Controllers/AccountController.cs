using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WorkMangement.Controllers
{
    public class AccountController : Controller
    {
        // Quản lý user
        private readonly UserManager<ApplicationUser> userManager;
        // Quản lý đăng nhập
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Đến trang đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                returnUrl = returnUrl
            };
            return View(model);
        }

        /// <summary>
        /// Xử lý đăng nhập
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // Kiểm tra returnUrl có rỗng hay không
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("","Tài khoản hoặc mật khẩu không đúng");
            }
            return View();
        }

        /// <summary>
        /// Đến trang đăng ký tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Xử lý đăng ký tài khoản
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    EmployeeCode = model.EmployeeCode,
                    Age = model.Age,
                    Department = model.Department,
                    IsMale = model.Gender == EnumGender.Nam ? true : false
                };

                // Create new user
                var result =await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Return to Login View
                    ViewData["RegisterSuccess"] = "Register Success";
                    return View("Login");
                }

                ModelState.AddModelError("", "Tạo tài khoản không thành công");
            }
            return View();
        }

        /// <summary>
        /// Đăng xuất
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }

        /// <summary>
        /// Xử lý user không có quyền truy cập dịch vụ
        /// Nguyễn Đình Hoàng 20173143
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}