//-----------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------


namespace FundooNote.Controllers
{
    using System;
    using BusinessLayer.Interfaces;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBL">The notes bl.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }


        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesCreate">The notes create.</param>
        /// <returns>note</returns>
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
        [HttpGet("Get")]
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Notes>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Notes>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<Notes>)notesBL.GetNotesTableData();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
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

