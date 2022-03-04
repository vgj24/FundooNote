using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
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
        private readonly INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }

        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateNotes(UserNotesData notesCreate)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.CreateNotes(notesCreate, userId);
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
        [HttpPut("Update")]
        public IActionResult UpdateNotes(UpdateModel notesUpdate, long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.UpdateNotes(notesUpdate, notesId);
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

        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.DeleteNotes(notesId);
                if (result)
                    return this.Ok(new { Success = true, message = "Deleted", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Not Deleted" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("{ID}/Get")]
        public IEnumerable<Notes> GetAllNotes(long userId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.GetAllNotes(userId);
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
        [Authorize]
        [HttpGet("GetNotesTableData")]
        public IEnumerable<Notes> GetNotesTableData()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.GetNotesTableData();
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

        [Authorize]
        [HttpPut("IsPin")]
        public IActionResult IsPinSetting(long userId, long NoteId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.IsPinSetting(userId, NoteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Updated", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "NotUpdated" });
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut("IsTrash")]
        public IActionResult ISTrashSetting(long userId, long NoteId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.ISTrashSetting(userId, NoteId);
                if (result!=null)
                    return this.Ok(new { Success = true, message = "Deleted", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Not Deleted" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("IsArchieve")]
        public IActionResult ISArchieveSetting(long userId, long NoteId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.ISArchieveSetting(userId,NoteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Deleted", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Not Deleted" });

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Color")]
        public IActionResult ColorChange(long noteID, string color)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.ColorChange(userId,noteID, color);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Color change successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Color change fail" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("Image")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.UploadImage(userId,noteId,image);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Image ulpoaded successfully", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "Image Upload fail" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}

