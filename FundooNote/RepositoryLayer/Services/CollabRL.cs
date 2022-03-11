//-----------------------------------------------------------------------
// <copyright file="CollabRL" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using global::RepositoryLayer.Context;
    using global::RepositoryLayer.Entity;
    using global::RepositoryLayer.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollabRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundoo context.</param>
        /// <param name="_appSettings">The application settings.</param>
        public CollabRL(FundooContext fundooContext, IConfiguration _appSettings)
        {
            this.fundooContext = fundooContext;
            this._appSettings = _appSettings;
        }

        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>AddsCollab</returns>
        public Collaborator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(e => e.Email == email);

                if (result.Email == email)
                {
                    Collaborator newcollab = new Collaborator();
                    newcollab.CollabEmail = email;
                    newcollab.Id = userId;
                    newcollab.NotesId = noteId;
                    fundooContext.CollboratorTable.Add(newcollab);
                    fundooContext.SaveChanges();
                    return newcollab;
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
        /// Deletes the collab.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabid">The collabid.</param>
        /// <returns>Labelinstance</returns>
        public bool DeleteCollab(long userId, long collabid)
        {
            try
            {
                var result = fundooContext.CollboratorTable.Where(e => e.Id == userId && e.CollabId == collabid).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.CollboratorTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                { 
                    return false; 
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
        /// <returns>GetAllCollabs</returns>
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.CollboratorTable.Where(e => e.NotesId == noteId).ToList();
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
        /// <returns>GetCollabTableData</returns>
        public IEnumerable<Collaborator> GetCollabTableData()
        {
            try
            {
                var result = this.fundooContext.CollboratorTable.ToList();
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
    }
}