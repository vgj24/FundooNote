//-----------------------------------------------------------------------
// <copyright file="NotesRL" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Linq;
    using global::RepositoryLayer.Interfaces;
    using global::RepositoryLayer.Context;
    using global::RepositoryLayer.Entity;
    using CommonLayer;

    public class NotesRL:INotesRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundoo context.</param>
        /// <param name="_appSettings">The application settings.</param>
        public NotesRL(FundooContext fundooContext, IConfiguration _appSettings)
        {
            this.fundooContext = fundooContext;
            this._appSettings = _appSettings;
        }

        /// <summary>
        /// Creates the notes.
        /// </summary>
        /// <param name="notesCreate">The notes create.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Noteinstance</returns>
        public Notes CreateNotes(UserNotesData notesCreate, long userId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.Title = notesCreate.Title;
                newNotes.Description = notesCreate.Description;
                newNotes.Remainder = notesCreate.Remainder;
                newNotes.Color = notesCreate.Color;
                newNotes.Image = notesCreate.Image;
                newNotes.IsArchive = notesCreate.IsArchive;
                newNotes.IsTrash = notesCreate.IsTrash;
                newNotes.IsPin = notesCreate.IsPin;
                newNotes.ModifiedAt = notesCreate.ModifiedAt;
                newNotes.CreateAt = notesCreate.CreateAt;
                newNotes.Id = userId;
                fundooContext.NotesTable.Add(newNotes);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return newNotes;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

              throw new AppException("Email or password is incorrect");

            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteUpdate">The note update.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>result</returns>
        public Notes UpdateNotes(UpdateModel noteUpdate, long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.NotesId == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteUpdate.Title;
                    result.Description = noteUpdate.Description;
                    result.Color = noteUpdate.Color;
                    result.Image = noteUpdate.Image;
                    fundooContext.NotesTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    // return null;
                    throw new AppException("Invalid noteid");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>booleanvalue</returns>
        public bool DeleteNotes(long noteId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.NotesId == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    //return false;
                    throw new AppException("Invalid noteid");

                }
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
        /// <returns>list</returns>
        public IEnumerable<Notes> GetAllNotes(long userId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.Id == userId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    //return null;
                    throw new AppException("Invlalid userId");

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Gets the notes table data.
        /// </summary>
        /// <returns>list</returns>
        public IEnumerable<Notes> GetNotesTableData()
        {
            try
            {
                var result = fundooContext.NotesTable.ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    // return null;
                    throw new AppException("No Notes yet");

                }
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
        /// <returns>notesinstance</returns>
        public Notes IsPinSetting(long userId, long NoteId)
        {
            try
            {
                Notes newNote = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NoteId);
                if (newNote != null)
                {
                    bool checkpin = newNote.IsPin;
                    if (checkpin == true)
                    {
                        newNote.IsPin = false;
                    }
                    if (checkpin == false)
                    {
                        newNote.IsPin = true;
                    }
                    fundooContext.SaveChanges();
                    return newNote;
                }
                else
                {
                    //return null;
                    throw new AppException("NO note found");
                }
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
        /// <returns>notesinstance</returns>
        public Notes ISTrashSetting(long userId, long NoteId)
        {
            try
            {
                Notes newNote = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NoteId);
                if (newNote != null)
                {
                    bool checktrash = newNote.IsTrash;
                    if (checktrash == true)
                    {
                        newNote.IsTrash = false;
                    }
                    if (checktrash == false)
                    {
                        newNote.IsTrash = true;
                    }
                    fundooContext.SaveChanges();
                    return newNote;
                }
                else
                {
                    //return null; 
                    throw new AppException("NO note found");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Determines whether /[is archieve setting] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns>notesinstance</returns>
        public Notes ISArchieveSetting(long userId, long NoteId)
        {
            try
            {
                Notes newNote = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == NoteId);
                if (newNote != null)
                {
                    bool checkArchieve = newNote.IsArchive;
                    if (checkArchieve == true)
                    {
                        newNote.IsArchive = false;
                    }
                    if (checkArchieve == false)
                    {
                        newNote.IsArchive = true;
                    }
                    fundooContext.SaveChanges();
                    return newNote;
                }
                else
                {
                    // return null;
                    throw new AppException("NO note found");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Colors the change.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>newnote</returns>
        public Notes ColorChange(long userId, long noteID, string color)
        {
            try
            {
                Notes newNote = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == noteID);
                if (newNote != null)
                {
                    newNote.Color = color;
                    fundooContext.SaveChanges();
                    return newNote;
                }
                else
                {
                    // return null;
                    throw new AppException("NO note found");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>instance</returns>
        public Notes UploadImage(long userId, long noteId, IFormFile image)
        {
            try
            {
                Notes newNote = fundooContext.NotesTable.FirstOrDefault(x => x.Id == userId && x.NotesId == noteId);
                if (newNote != null)
                {
                    Account account = new Account(
                       "vgj24cloud",
                       "965367769551412",
                       "0ByfvCupwo1vndWrB-A7ES89Blg");

                    Cloudinary cloudinary = new Cloudinary(account);

                    var imagepath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagepath),
                    };
                    var result = cloudinary.Upload(uploadParams);
                    newNote.Image = image.FileName;
                    fundooContext.NotesTable.Update(newNote);
                    fundooContext.SaveChanges();
                    return newNote;
                }
                else
                {
                    //return null;
                    throw new AppException("There is some problem in uploading image");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
