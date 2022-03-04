using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {
        public Notes CreateNotes(UserNotesData notesCreate, long userId);
        public Notes UpdateNotes(UpdateModel noteUpdate, long noteId);
        public bool DeleteNotes(long noteId);
        public IEnumerable<Notes> GetAllNotes(long userId);
        public IEnumerable<Notes> GetNotesTableData();
        public Notes IsPinSetting(long userId, long NoteId);
        public Notes ISTrashSetting(long userId, long NoteId);
        public Notes ISArchieveSetting(long userId, long NoteId);
        public Notes ColorChange(long userId, long noteID, string color);
        public Notes UploadImage(long userId, long noteId, IFormFile image);

    }
}
