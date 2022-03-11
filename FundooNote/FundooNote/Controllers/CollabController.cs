//-----------------------------------------------------------------------
// <copyright file="CollabController.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooNote.Controllers
{
    using System;
    using BusinessLayer.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using BusinessLayer.Interfaces;
    using System.Text;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// /
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabratorBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabController"/> class.
        /// </summary>
        /// <param name="collabratorBL">The collabrator bl.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollabController(ICollabBL collabratorBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabratorBL = collabratorBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns><AddsCollab/returns>
       [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.AddCollab(email, userId, noteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collab Added", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Noone Collabed" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="collabId">The collab identifier.</param>
        /// <returns>collab</returns>
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.DeleteCollab(userId, collabId);
                if (result)
                {
                    return this.Ok(new { Success = true, message = "Collab Removed", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = " Collab not Removed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all collabs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list</returns>
        [Authorize]
        [HttpGet("{ID}/Collabrations")]
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = collabratorBL.GetAllCollabs(userId, noteId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the collab table data.
        /// </summary>
        /// <returns>list</returns>
        [Authorize]
        [HttpGet("Get")]
        public IEnumerable<Collaborator> GetCollabTableData()
        {
            try
            {
                var result = collabratorBL.GetCollabTableData();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Radis server
        /// </summary>
        /// <returns>cache</returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<Collaborator>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<Collaborator>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<Collaborator>)collabratorBL.GetCollabTableData();
                serializedCustomerList = JsonConvert.SerializeObject(customerList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(customerList);
        }
    }
}


