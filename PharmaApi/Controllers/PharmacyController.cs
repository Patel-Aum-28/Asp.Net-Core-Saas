using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaApi.Data;
using PharmaApi.Models;
using PharmaApi.Repository;
using PharmaApi.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Xml.Linq;

namespace PharmaApi.Controllers
{
    [ApiController]
    [Authorize]
    public class PharmacyController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;
        public PharmacyController(JwtTokenHandler jwtTokenHandler, IPasswordService passwordService, IMapper mapper, IUserRepository userRepository)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _passwordService = passwordService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            try
            {
                var userList = await _userRepository.GetAllAsync();

                var users = _mapper.Map<List<UserViewModel>>(userList);
                
                return Ok(new { success = true, result = users, Message = "User List." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, err = $"Error: {ex.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserTable model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = await _userRepository.GetAllAsync();

                    foreach (var user in users)
                    {
                        if (user.Email == model.Email)
                        {
                            return BadRequest(new { success = false, err = "Email already exist use different one." });
                        }
                        if (user.MobileNo == model.MobileNo)
                        {
                            return BadRequest(new { success = false, err = "Mobile number already exist use different one." });
                        }
                    }

                    model.PasswordHash = _passwordService.GetHashPassword(model.PasswordHash);
                    model.CreatedAt = DateTime.Now;
                    model.UpdatedAt = DateTime.Now;
                    var result = await _userRepository.AddAsync(model);

                    return Ok(new { success = true, result = result, Message = "User Added Successfully." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { success = false, err = $"Error: {ex.Message}" });
                }
            }
            return BadRequest(new { success = false, err = "Invalid model." });
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);

                if (user == null)
                {
                    return BadRequest(new { success = false, err = "User does not exist." });
                }

                var userView = _mapper.Map<UserViewModel>(user);

                return Ok(new { success = true, result = userView, Message = "User data." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, err = $"Error: {ex.Message}" });
            }
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(UserTable model)
        {
            ModelState.Remove("PasswordHash");
            if (ModelState.IsValid)
            {
                try
                {
                    var users = await _userRepository.GetAllAsync();
                    string currentPasswordHash = "";
                    DateTime currentCreatedAt = DateTime.Now;

                    foreach (var user in users)
                    {
                        if(user.UserId != model.UserId)
                        {
                            if(user.Email == model.Email)
                            {
                                return BadRequest(new { success = false, err = "Email already exist use different one." });
                            }
                            if (user.MobileNo == model.MobileNo)
                            {
                                return BadRequest(new { success = false, err = "Mobile number already exist use different one." });
                            }
                        }
                        else if(user.UserId == model.UserId)
                        {
                            currentPasswordHash = user.PasswordHash;
                            currentCreatedAt = user.CreatedAt ?? currentCreatedAt;
                        }
                        else
                        {
                            return BadRequest(new { success = false, err = $"User not found with provided id = {model.UserId}." });
                        }
                    }

                    model.PasswordHash = currentPasswordHash;
                    model.CreatedAt = currentCreatedAt;
                    var result = _userRepository.Update(model);

                    return Ok(new { success = true, result = result, Message = "User Updated Successfully." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { success = false, err = $"Error: {ex.Message}" });
                }
            }
            return BadRequest(new { success = false, err = "Invalid model." });
        }

    }
}
