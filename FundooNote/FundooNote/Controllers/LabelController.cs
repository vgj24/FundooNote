//-----------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNote.Controllers
{
    using System;
    using BusinessLayer.Interfaces;
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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelBL">The label bl.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelController(ILabelBL labelBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>label</returns>
        [Authorize]
        [HttpPost("Create")]
        public IActionResult AddLabel(string labelName, long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.AddLabel(labelName, userId, noteId);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Label Added", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "label not added" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updatelabels the specified label name.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>updatedlabel</returns>
        [Authorize]
        [HttpPost("Update")]
        public IActionResult Updatelabel(string labelName, long labelID)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.Updatelabel(labelName, userId, labelID);
                if (result != null)
                    return this.Ok(new { Success = true, message = "Label Updated", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "label not Updated" });
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>removelabel</returns>
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.Deletelabel(labelId);
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

        /// <summary>
        /// Labelsbies the userid.
        /// </summary>
        /// <returns>list</returns>
        [Authorize]
        [HttpGet("{ID}/Get")]
        public IEnumerable<label> labelsbyUserid()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.GetAllLabelbyUserid(userId);
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

        /// <summary>
        /// Labelsbies the noteid.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list</returns>
        [Authorize]
        [HttpGet("Get")]
        public IEnumerable<label> labelsbyNoteid(long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = labelBL.GetAllLabelbyNoteid(noteId);
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

        /// <summary>
        /// Gets the label table data.
        /// </summary>
        /// <returns>getallalbels</returns>
        [Authorize]
        [HttpGet("GetAll")]
        public IEnumerable<label> GetLabelTableData()
        {
            try
            {
                var result = labelBL.GetLabelTableData();
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

        /// <summary>
        /// Radis server
        /// </summary>
        /// <returns>cache</returns>
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "customerList";
            string serializedCustomerList;
            var customerList = new List<label>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<label>>(serializedCustomerList);
            }
            else
            {
                customerList = (List<label>)labelBL.GetLabelTableData();
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
