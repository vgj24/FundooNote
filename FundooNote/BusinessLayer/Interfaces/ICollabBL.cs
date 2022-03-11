// -----------------------------------------------------------------------
// <copyright file="ICollabBL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Interfaces
{
    using System;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;

    public interface ICollabBL
    {
        /// <summary>
        /// Adds the collab.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>addcollab</returns>
        public Collaborator AddCollab(string email, long userId, long noteId);

        /// <summary>
        /// Deletes the collab.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="collabid">The collabid.</param>
        /// <returns>booleanvalue</returns>
        public bool DeleteCollab(long userId, long collabid);

        /// <summary>
        /// Gets all collabs.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId);

        /// <summary>
        /// Gets the collab table data.
        /// </summary>
        /// <returns>list</returns>
        public IEnumerable<Collaborator> GetCollabTableData();

    }
}
