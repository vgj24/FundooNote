//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooNote.Controllers
{
    using System;
    using BusinessLayer.Interfaces;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registers the specified regist.
        /// </summary>
        /// <param name="regist">The regist.</param>
        /// <returns></returns>
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

        /// <summary>
        /// User Login Api
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>login</returns>
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

        /// <summary>
        /// Forgot Password API
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>password</returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string Email)
        {
            try
            {
                var token = userBL.ForgotPassword(Email);
                if (token != null)
                    return this.Ok(new { Success = true, message = "Email Sent Succesfully", data = token });
                else
                    return this.BadRequest(new { Success = false, message = "Email not sent"});
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// User  Reset PAssword
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns>password</returns>

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string Password,string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userBL.ResetPassword(email, Password, confirmPassword))
                    return this.Ok(new { Success = true, message = "Password Changed Successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Unable to Reset Password" });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}



