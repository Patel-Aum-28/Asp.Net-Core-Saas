using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmaApi.Data;
using PharmaApi.Models;
using PharmaApi.Services;

namespace PharmaApi.Controllers
{
    [ApiController]
    public class MasterController : Controller
    {
        private readonly MasterDbContext _masterDbContext;
        private readonly IPasswordService _passwordService;
        private readonly JwtTokenHandler _jwtTokenHandler;

        public MasterController(MasterDbContext masterDbContext, IPasswordService passwordService, JwtTokenHandler jwtTokenHandler)
        {
            _masterDbContext = masterDbContext;
            _passwordService = passwordService;
            _jwtTokenHandler = jwtTokenHandler;
        }
        // Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, err = "Invalid model." });
                }

                var pharmaDetail = _masterDbContext.MasterTable.FirstOrDefault(u => u.Email == model.Email.ToLower() && u.IsActive == true);
                if (pharmaDetail == null)
                {
                    return BadRequest(new { success = false, err = "Email does not exist or User is InActive." });
                }

                bool verifyPassword = _passwordService.VerifyHash(model.PasswordHash, pharmaDetail.PasswordHash);
                if (verifyPassword)
                {
                    string tokenValue = _jwtTokenHandler.GenerateToken(pharmaDetail);

                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        return BadRequest(new { success = false, err = "Error while generating token." });
                    }

                    HttpContext.Session.SetString("JwtToken", tokenValue);
                    HttpContext.Session.SetString("PharmacyName", pharmaDetail.PharmacyName);

                    return Ok(new { success = true, result = tokenValue, Message = "LoggedIn successfully." });
                }
                else
                {
                    return BadRequest(new { success = false, err = "Invalid Email or Password" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, err = $"Error: {ex.Message}" });
            }
        }
        [HttpPost("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Ok(new { success = true, result = "Successfully LogedOut." }); ;
        }

    }
}
