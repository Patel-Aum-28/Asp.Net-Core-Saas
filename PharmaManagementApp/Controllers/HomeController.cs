using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementApp.Services;
using PharmaManagementApp.Data;
using PharmaManagementApp.Models;
using PharmaManagementApp.Services;

namespace PharmaManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly MasterDbContext _masterDbContext;
        private readonly IPasswordService _passwordService;
        private readonly JwtTokenHandler _jwtTokenHandler;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, MasterDbContext masterDbContext, IPasswordService passwordService, JwtTokenHandler jwtTokenHandler)
        {
            _logger = logger;
            _configuration = configuration;
            _masterDbContext = masterDbContext;
            _passwordService = passwordService;
            _jwtTokenHandler = jwtTokenHandler;
        }
        // Login
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ModelState);
                }

                var pharmaDetail = _masterDbContext.MasterTable.FirstOrDefault(u => u.Email == model.Email.ToLower() && u.IsActive == true);
                if (pharmaDetail == null)
                {
                    TempData["ErrorMessage"] = "Email does not exist or User is InActive.";
                    return View();
                }

                bool verifyPassword = _passwordService.VerifyHash(model.PasswordHash, pharmaDetail.PasswordHash);
                if (verifyPassword)
                {
                    string tokenValue = _jwtTokenHandler.GenerateToken(pharmaDetail);

                    if (string.IsNullOrEmpty(tokenValue))
                    {
                        TempData["ErrorMessage"] = "Error while generating token.";
                        return View(model);
                    }

                    HttpContext.Session.SetString("JwtToken", tokenValue);
                    HttpContext.Session.SetString("PharmacyName", pharmaDetail.PharmacyName);

                    TempData["SuccessMessage"] = "LoggedIn Successfully!";
                    return RedirectToAction("Index", "Pharmacy");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Email or Password.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                return View(model);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
