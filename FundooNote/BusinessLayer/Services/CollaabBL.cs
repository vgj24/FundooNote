// -----------------------------------------------------------------------
// <copyright file="CollaabBL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using System;
    using BusinessLayer.Interfaces;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interfaces;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Services;


    public class CollaabBL : ICollabBL
    {
        private readonly ICollabRL collabratorRL;
        /// <summary>
        /// Initializes a new instance of the <see cref="CollaabBL"/> class.
        /// </summary>
        /// <param name="collabratorRL">The collabrator rl.</param>
        public CollaabBL(ICollabRL collabratorRL)
        {
            this.collabratorRL = collabratorRL;
        }
        /// <summary>
        /// add collab
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns>Collaboration</returns>
        
        public Collaborator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                return collabratorRL.AddCollab(email, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove Collab
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="collabid"></param>
        /// <returns>booleanvalue</returns>
        
        public bool DeleteCollab(long userId, long collabid)
        {
            try
            {
                return collabratorRL.DeleteCollab(userId, collabid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get All Collabs 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns>list</returns>
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId)
        {
            try
            {
                return collabratorRL.GetAllCollabs(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  GetCollabTableData
        /// </summary>
        /// <returns></returns>
        //// GetCollabTableData
        public IEnumerable<Collaborator> GetCollabTableData()
        {
            try
            {
                return collabratorRL.GetCollabTableData();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}


