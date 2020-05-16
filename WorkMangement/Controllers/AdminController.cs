using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WorkMangement.Controllers
{
    public class AdminController : Controller
    {
        private readonly WorkDL workDL;
        private readonly PhaseDL phaseDL;
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(SignInManager<ApplicationUser> signinManager, UserManager<ApplicationUser> userManager, ApplicationDBContext context)
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            workDL = new WorkDL(context);
            phaseDL = new PhaseDL(context);
        }

        
    }
}