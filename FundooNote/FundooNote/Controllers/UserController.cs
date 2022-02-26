using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;
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
    }
}
