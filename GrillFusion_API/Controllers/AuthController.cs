using GrillFusion_API.Models;
using GrillFusion_API.Models.Dto;
using GrillFusion_API.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace GrillFusion_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string secretKey;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? "";
            _response = new ApiResponse();
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    NormalizedEmail = model.Email.ToUpper(),
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    // 🔧 FIX 1 - Check each role independently so no role is ever skipped
                    // Old code checked only if Admin existed, then created both in one block
                    // If Admin existed but Customer was deleted, Customer would never be recreated
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));

                    if (model.Role.Equals(SD.Role_Admin, StringComparison.CurrentCultureIgnoreCase))
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Customer);
                    }

                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _response.ErrorMessages.Add(error.Description);
                    }
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
            }
            else
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var error in ModelState.Values)
                {
                    foreach (var item in error.Errors)
                    {
                        _response.ErrorMessages.Add(item.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                _response.Result = new LoginResponseDTO();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Invalid State");
                return BadRequest(_response);
            }

            var userFromDb = await _userManager.FindByEmailAsync(model.Email);

            // 🔧 FIX 2 - Explicitly return 400 if user not found
            // Old code: if (userFromDb != null) { ... } then silently fell to bottom catch-all
            // Now we return early with a clear message, rest of code runs without nesting
            if (userFromDb == null)
            {
                _response.Result = new LoginResponseDTO();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Incorrect credentials. Check your email/password");
                return BadRequest(_response);
            }

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
            if (!isValid)
            {
                _response.Result = new LoginResponseDTO();
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Incorrect credentials. Check your email/password");
                return BadRequest(_response);
            }

            // 🔧 FIX 3 & 4 - Fetch roles once with await, store in variable, reuse twice
            // Old code: called GetRolesAsync(user).Result twice — blocked the thread (deadlock risk)
            // and made two separate DB round trips unnecessarily
            var roles = await _userManager.GetRolesAsync(userFromDb);
            var userRole = roles.FirstOrDefault() ?? SD.Role_Customer;

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity([
                    new ("fullname", userFromDb.Name),
                    new ("id", userFromDb.Id),
                    new (ClaimTypes.Email, userFromDb.Email!.ToString()),
                    new (ClaimTypes.Role, userRole)  // 🔧 FIX 3 - using awaited variable
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponse = new()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token),
                Role = userRole  // 🔧 FIX 4 - reusing same variable, no second DB call
            };

            _response.Result = loginResponse;
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
    }
}