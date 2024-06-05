using APP_DATA.DatabaseContext;
using APP_DATA.DTO;
using APP_DATA.Enums;
using APP_DATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
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

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _db.User.ToListAsync();
            return Ok(user);
        }

        [HttpGet("get-user-by-id")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(_db.User.FirstOrDefault(temp => temp.Id == id));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                return Ok(loginDTO);
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
            return Ok(loginDTO);
        }

        [HttpPut("update-user")]
        public IActionResult UserChatLieu(ApplicationUser user)
        {
            try
            {
                ApplicationUser? matchingUser = _db.User.FirstOrDefault(temp => temp.Id == user.Id);

                if (matchingUser == null)
                    throw new ArgumentNullException(nameof(ApplicationUser));

                matchingUser.Email = user.Email;
                

                _db.User.Update(matchingUser);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
