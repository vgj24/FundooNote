using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistration regist)
        {
            try
            {
                var result = userBL.Registration(regist);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Registration successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Registration Unsuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //User Login Api
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = userBL.Login(userLogin.Email, userLogin.Password);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Logged In", data = user });
                else
                    return this.BadRequest(new { Success = false, message = "Enter Valid Email and Password" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                var user = userBL.ForgotPassword(Email);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Email Sent Succesfully", data = user });
                else
                    return this.BadRequest(new { Success = false, message = "Email not sent"});
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

