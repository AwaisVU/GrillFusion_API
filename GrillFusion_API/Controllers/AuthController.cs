using GrillFusion_API.Models;
using GrillFusion_API.Models.Dto;
using GrillFusion_API.Utility;
using GrillFusion_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        private readonly IConfiguration _config;
        private readonly EmailService _emailService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EmailService emailService)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? "";
            _response = new ApiResponse();
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _config = configuration;
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
                    // 1 - Check each role independently so no role is ever skipped
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


        [HttpPost("login")] //
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



        //Password Management 

        //Forgot Password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Ok();
            // 👆 Don't reveal if email exists

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(token)
            );

            var resetLink = $"{_config["App:FrontendUrl"]}/reset-password?email={model.Email}&token={encodedToken}";
            // 👆 THIS must match your React route

            var emailBody = $@"
        <h3>Password Reset</h3>
        <p>Click below to reset your password:</p>
        <a href='{resetLink}'>Reset Password</a>
    ";

            await _emailService.SendEmailAsync(
                model.Email,
                "Reset Your Password",
                emailBody
            );

            return Ok();
        }


        //Reset Password
        [HttpPost("reset-password")]
public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
{

if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

    if(string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.NewPassword) || string.IsNullOrWhiteSpace(model.Token))
            {
                return BadRequest("Request is invalid");
            }

    var user = await _userManager.FindByEmailAsync(model.Email);

    if (user == null)
        return BadRequest("Invalid request");

    string decodedToken;

try
{
    decodedToken = Encoding.UTF8.GetString(
        WebEncoders.Base64UrlDecode(model.Token)
    );
}
catch
{
    return BadRequest("Invalid token");
}


    var result = await _userManager.ResetPasswordAsync(
        user,
        decodedToken,
        model.NewPassword
    );

    if (!result.Succeeded)
        return BadRequest(result.Errors);

    return Ok("Password reset successful");
}
    }
}