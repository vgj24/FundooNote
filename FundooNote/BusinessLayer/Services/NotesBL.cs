//-----------------------------------------------------------------------
// <copyright file="UserBL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using System;
    using BusinessLayer.Interfaces;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interfaces;
    using RepositoryLayer.Services;
    using System.Collections.Generic;
    using System.Text;
    public class NotesBL:INotesBL
    { 
        private readonly INotesRL notesRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesBL"/> class.
        /// </summary>
        /// <param name="notesRL">The notes rl.</param>
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesCreate">The notes create.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>note</returns>
        public Notes CreateNotes(UserNotesData notesCreate, long userId)
        {
            try
            {

                return notesRL.CreateNotes(notesCreate, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteUpdate">The note update.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>updatednote</returns>
        public Notes UpdateNotes(UpdateModel noteUpdate, long noteId)
        {
            try
            {
                return notesRL.UpdateNotes(noteUpdate, noteId);


            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// DeleteNotes
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>boolenvalue</returns>

        public bool DeleteNotes(long noteId)
        {
            try
            {
                return notesRL.DeleteNotes(noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>GetAllNotes</returns>
        public IEnumerable<Notes> GetAllNotes(long userId)
        {
            try
            {
                return notesRL.GetAllNotes(userId);
                ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the notes table data.
        /// </summary>
        /// <returns>GetNotesTableData</returns>
        public IEnumerable<Notes> GetNotesTableData()
        {
            try
            {
                return notesRL.GetNotesTableData();

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Determines whether [is pin setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>IsPinSetting</returns>
        public Notes IsPinSetting(long userId, long NoteId)
        {
            try
            {
                return notesRL.IsPinSetting(userId, NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Determines whether [is trash setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>ISTrashSetting</returns>
        public Notes ISTrashSetting(long userId, long NoteId)
        {
            try
            {
                return notesRL.ISTrashSetting(userId, NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="NoteId"></param>
        /// <returns>ISArchieveSetting</returns>
        public Notes ISArchieveSetting(long userId, long NoteId)
        {
            try
            {
                return notesRL.ISArchieveSetting(userId, NoteId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// color change
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteID"></param>
        /// <param name="color"></param>
        /// <returns>notecolorstring</returns>

        public Notes ColorChange(long userId, long noteID, string color)
        {
            try
            {
                return notesRL.ColorChange(userId, noteID, color);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// add image
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <param name="image"></param>
        /// <returns>uploadimage</returns>
        
        public Notes UploadImage(long userId, long noteId, IFormFile image)
        {
            try
            {
                return notesRL.UploadImage(userId, noteId, image);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
