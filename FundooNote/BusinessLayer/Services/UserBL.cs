using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        //instance of RepoLayer Interface
        private readonly IUserRL userRL;
        //Constructor
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }


        //User registration
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                return userRL.Registration(userRegist);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //User Login
        public string Login(string email, string password)
        {
            try
            {
                return userRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }

        }
        //Forgot Email

        public string ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

