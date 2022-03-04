using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace FundooNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly CollaabBL collabratorBL;
        public CollabController(CollaabBL collabratorBL)
        {
            this.collabratorBL = collabratorBL;
        }
        [Authorize]
        [HttpPost("Collab")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.AddCollab(email, userId, noteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Collab Added", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Noone Collabed" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("Remove Collab")]
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.DeleteCollab(userId, collabId);
                if (result)
                    return this.Ok(new { Success = true, message = "Collab Removed", data = result });
                else
                    return this.BadRequest(new { Success = false, message = " Collab not Removed" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("{ID}/Collabrations")]
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.GetAllCollabs(userId, noteId);
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}


