﻿using APP_DATA.DatabaseContext;
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

        public TaiKhoanController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, AppDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _db.User.ToListAsync();
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PersonName = registerDTO.PersonName,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
                Status = StatusOptions.Active.ToString()
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                //Kiem tra trang thai radio btn
                if (registerDTO.UserType == APP_DATA.Enums.UserTypeOptions.Admin)
                {
                    //Tao role admin
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole()
                        {
                            Name = UserTypeOptions.Admin.ToString()
                        };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    //Them user moi trong role admin
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    //Tao role user
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    //Them user moi trong role user
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }
                //Tao cart cho user
                var gioHang = new GioHang()
                {
                    ID_User = user.Id,
                    TrangThai = StatusOptions.Active.ToString()
                };

                _db.GioHang.Add(gioHang);
                await _db.SaveChangesAsync();

                return RedirectToAction("Login", "TaiKhoan");
            }
            else
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("Register", item.Description);
                }
            }

            return View(registerDTO);
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

        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        //[HttpGet]
        //public IActionResult Edit(Guid AccountID)
        //{
        //    //var matchingAccount = _repo.GetById(AccountID);

        //    //if (matchingAccount == null)
        //    //    return RedirectToAction("Index");

        //    //return View(matchingAccount);
        //    return View(temp);
        //}

        //[HttpPost]
        //public IActionResult Edit(ApplicationUser user)
        //{
        //    //var matchingAccount = _db.User.FirstOrDefaultAsync(temp => temp.Id == user.Id);

        //    //if (matchingAccount == null)
        //    //    return RedirectToAction("Index");

        //    //if (ModelState.IsValid)
        //    //{
        //    //    _db.User.Update(user);
        //    //    return RedirectToAction("Index");
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Error = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
        //    //    return View(matchingAccount);
        //    //}

        //    _repo.UpdateObj(user);
        //    return RedirectToAction("Index");
        //}
    }
}