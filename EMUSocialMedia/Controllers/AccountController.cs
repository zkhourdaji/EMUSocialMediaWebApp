using System;
using System.Linq;
using System.Threading.Tasks;
using EMUSocialMedia.Identity;
using EMUSocialMedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EMUSocialMedia.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<MyIdentityUser> _userManager;
        private readonly SignInManager<MyIdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MyIdentityContext _identityContext;


        public AccountController(
        UserManager<MyIdentityUser> userManager,
        SignInManager<MyIdentityUser> signinManager,
        RoleManager<IdentityRole> roleManager,
        MyIdentityContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel signupViewModel)
        {
            MyIdentityUser user = new MyIdentityUser()
            {
                Email = signupViewModel.Email,
                UserName = signupViewModel.Email
            };

            IdentityResult result = _userManager.CreateAsync(user, signupViewModel.Password).Result;
            if (result.Succeeded)
            {
                _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return Content("User account creation failed: " + result.Errors.First().Description);
            }
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SigninViewModel signinViewModel)
        {
            MyIdentityUser user = await _userManager.FindByEmailAsync(signinViewModel.Email);
            if (user != null)
            {
                SignInResult result = await _signInManager.CheckPasswordSignInAsync(
                user, signinViewModel.Password, false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    string role;
                    if (roles.Count != 0)
                        role = roles.First().ToString();
                    else
                        role = "None";
                    _signInManager.SignInAsync(user, true).Wait();
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    return Content("Failed to login");
                }
            }
            else
            {
                return Content("Failed to login. User doesnt exist");
            }
        }


        public IActionResult Signout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
