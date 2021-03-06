// -----------------------------------------------------------------------
// <copyright file="INotesBL.cs" company="Vrushali">
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
    using System.Text;
    public interface INotesBL
    {
        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesCreate">The notes create.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note</returns>
        public Notes CreateNotes(UserNotesData notesCreate, long userId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteUpdate">The note update.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>updtednote</returns>
        public Notes UpdateNotes(UpdateModel noteUpdate, long noteId);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>boolenvalue</returns>
        public bool DeleteNotes(long noteId);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>list</returns>
        public IEnumerable<Notes> GetAllNotes(long userId);

        /// <summary>
        /// Gets the notes table data.
        /// </summary>
        /// <returns>list</returns>
        public IEnumerable<Notes> GetNotesTableData();

        /// <summary>
        /// Determines whether [is pin setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>IsPinSetting</returns>
        public Notes IsPinSetting(long userId, long NoteId);

        /// <summary>
        /// Determines whether [is trash setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>ISTrashSetting</returns>
        public Notes ISTrashSetting(long userId, long NoteId);

        /// <summary>
        /// Determines whether [is archieve setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>ISArchieveSetting</returns>
        public Notes ISArchieveSetting(long userId, long NoteId);

        /// <summary>
        /// Colors the change.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>note</returns>
        public Notes ColorChange(long userId, long noteID, string color);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>note</returns>
        public Notes UploadImage(long userId, long noteId, IFormFile image);

    }
}
