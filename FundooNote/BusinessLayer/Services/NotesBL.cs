using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
  public class NotesBL:INotesBL
    { 
        private readonly INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        //create Notes

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
        //Update Notes

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
        //delete Notes

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
        //getall notes Notes

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
        //get notestable data Notes

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
        //isping set
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
        //is trsh set
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
        //isarchieve set
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
        //color change
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
        //add image
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
