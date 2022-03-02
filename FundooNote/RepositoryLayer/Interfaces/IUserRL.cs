using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public User Registration(UserRegistration userRegist);
        public string Login(string email, string password);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string Email, string Password, string confirmPassword);
        public Notes CreateNotes(UserNotesData notesCreate, long userId);
        public Notes UpdateNotes(UserNotesData noteUpdate, long noteId,long userId);



    }
}
