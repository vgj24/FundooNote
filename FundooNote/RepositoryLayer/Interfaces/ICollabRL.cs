//-----------------------------------------------------------------------
// <copyright file="ICollabRL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interfaces
{
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;

   public interface ICollabRL
    {
        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>AddCollab</returns>
        public Collaborator AddCollab(string email, long userId, long noteId);

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabid">The collabid.</param>
        /// <returns>DeleteCollab</returns>
        public bool DeleteCollab(long userId, long collabid);

        /// <summary>
        /// Gets all collabs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>allCollabList</returns>
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId);

        /// <summary>
        /// Gets the collab table data.
        /// </summary>
        /// <returns>GetCollabTableData</returns>
        public IEnumerable<Collaborator> GetCollabTableData();


    }
}
