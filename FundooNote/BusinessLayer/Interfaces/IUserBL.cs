using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
  public  interface IUserBL
    {
        public User Registration(UserRegistration userRegist);
        public string Login(string email, string password);
        public string ForgotPassword(string Email);
        public bool ResetPassword(string Email, string Password, string confirmPassword);
    }
}
