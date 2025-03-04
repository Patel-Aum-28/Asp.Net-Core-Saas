using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementApp.Services;
using PharmaManagementApp.Data;
using PharmaManagementApp.Models;
using PharmaManagementApp.Repository;
using PharmaManagementApp.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Xml.Linq;

namespace PharmaManagementApp.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly IDynamicDbContextFactory _dynamicDbContextFactory;
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        public PharmacyController(IDynamicDbContextFactory dynamicDbContextFactory, JwtTokenHandler jwtTokenHandler, IPasswordService passwordService, IMapper mapper)
        {
            _dynamicDbContextFactory = dynamicDbContextFactory;
            _jwtTokenHandler = jwtTokenHandler;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                bool loggedIn = IsLoggedin(out string message);
                if (!loggedIn)
                {
                    TempData["ErrorMessage"] = message;
                    return RedirectToAction("LogOut", "Home");
                }

                string connectionString = message;

                var userRepository = new UserRepository(_dynamicDbContextFactory, connectionString);
                var userService = new UserService(userRepository);

                var userList = await userService.GetAllUsersAsync();

                var users = _mapper.Map<List<UserViewModel>>(userList);
                
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                return View();
            }
        }

        public IActionResult Create()
        {
            try
            {
                bool loggedIn = IsLoggedin(out string message);
                if (!loggedIn)
                {
                    TempData["ErrorMessage"] = message;
                    return RedirectToAction("LogOut", "Home");
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserTable model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool loggedIn = IsLoggedin(out string message);
                    if (!loggedIn)
                    {
                        TempData["ErrorMessage"] = message;
                        return RedirectToAction("LogOut", "Home");
                    }

                    string connectionString = message;

                    var userRepository = new UserRepository(_dynamicDbContextFactory, connectionString);
                    var userService = new UserService(userRepository);
                    var users = await userService.GetAllUsersAsync();

                    foreach (var user in users)
                    {
                        if (user.Email == model.Email)
                        {
                            TempData["ErrorMessage"] = "Email already exist use different one.";
                            return View(model);
                        }
                        if (user.MobileNo == model.MobileNo)
                        {
                            TempData["ErrorMessage"] = "Mobile number already exist use different one.";
                            return View(model);
                        }
                    }

                    model.PasswordHash = _passwordService.GetHashPassword(model.PasswordHash);
                    model.CreatedAt = DateTime.Now;
                    model.UpdatedAt = DateTime.Now;
                    await userService.AddUserAsync(model);

                    TempData["SuccessMessage"] = "User successfully added.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                bool loggedIn = IsLoggedin(out string message);
                if (!loggedIn)
                {
                    TempData["ErrorMessage"] = message;
                    return RedirectToAction("LogOut", "Home");
                }

                string connectionString = message;

                var userRepository = new UserRepository(_dynamicDbContextFactory, connectionString);
                var userService = new UserService(userRepository);

                var user = await userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    TempData["SuccessMessage"] = "User does not exist.";
                    return RedirectToAction("Index");
                }

                var userView = _mapper.Map<UserViewModel>(user);
                return View(userView);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                bool loggedIn = IsLoggedin(out string message);
                if (!loggedIn)
                {
                    TempData["ErrorMessage"] = message;
                    return RedirectToAction("LogOut", "Home");
                }

                string connectionString = message;

                var userRepository = new UserRepository(_dynamicDbContextFactory, connectionString);
                var userService = new UserService(userRepository);
                var user = await userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    TempData["SuccessMessage"] = "Used does not exist.";
                    return RedirectToAction("Index");
                }
                user.PasswordHash = null;
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserTable model)
        {
            ModelState.Remove("PasswordHash");
            if (ModelState.IsValid)
            {
                try
                {
                    bool loggedIn = IsLoggedin(out string message);
                    if (!loggedIn)
                    {
                        TempData["ErrorMessage"] = message;
                        return RedirectToAction("LogOut", "Home");
                    }
                    
                    string connectionString = message;

                    var userRepository = new UserRepository(_dynamicDbContextFactory, connectionString);
                    var userService = new UserService(userRepository);

                    var users = await userService.GetAllUsersAsync();
                    string currentPasswordHash = "";
                    DateTime currentCreatedAt = DateTime.Now;

                    foreach (var user in users)
                    {
                        if(user.UserId != model.UserId)
                        {
                            if(user.Email == model.Email)
                            {
                                TempData["ErrorMessage"] = "Email already exist use different one.";
                                return View(model);
                            }
                            if (user.MobileNo == model.MobileNo)
                            {
                                TempData["ErrorMessage"] = "Mobile number already exist use different one.";
                                return View(model);
                            }
                        }
                        else
                        {
                            currentPasswordHash = user.PasswordHash;
                            currentCreatedAt = user.CreatedAt ?? currentCreatedAt;
                        }
                    }

                    model.PasswordHash = currentPasswordHash;
                    model.CreatedAt = currentCreatedAt;
                    userService.UpdateUser(model);

                    TempData["SuccessMessage"] = "User successfully Updated.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error occur: " + ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        private bool IsLoggedin(out string Message)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                Message = "Login to your account.";
                return false;
            }

            string dbName = _jwtTokenHandler.ValidateToken(token);
            if (string.IsNullOrEmpty(dbName))
            {
                Message = "Invalid Token, Token Expires or DbName not found, login again.";
                return false;
            }

            Message = $"Server=(localDb)\\localDB;Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
            return true;
        }
    }
}
