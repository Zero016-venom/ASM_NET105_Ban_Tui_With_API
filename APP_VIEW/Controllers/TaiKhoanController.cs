using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Enums;
using APP_DATA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP_VIEW.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly HttpClient httpClient;

        public TaiKhoanController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            string requestUrl = "https://localhost:7073/api/TaiKhoan/register";
            var response = await httpClient.PostAsJsonAsync(requestUrl, registerDTO);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Admin
                ApplicationUser user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                }
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Login", "Email hoặc mật khẩu không hợp lệ");
            return View(loginDTO);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "TaiKhoan");
        }

        //public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        //{
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    if (user == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json(false);
        //    }
        //}
    }
}
