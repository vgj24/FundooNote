using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
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
    public class NotesController : ControllerBase
    {
        private readonly IUserBL userBL;
        public NotesController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [Authorize]
        [HttpPost("CreateNotes")]
        public IActionResult CreateNotes(UserNotesData notesCreate)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = userBL.CreateNotes(notesCreate,userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Note Added", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Nothing saved" });
            }
            catch (Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPost("UpdateNotes")]
        public IActionResult UpdateNotes(UserNotesData notesUpdate, long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = userBL.UpdateNotes(notesUpdate, notesId,userId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Note updated", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "No note found" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

