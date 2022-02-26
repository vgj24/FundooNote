using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer;
using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;

namespace BusinessLayer.Services
{
    public class UserBL :IUserBL
    {
        private readonly UserRL userRL;
        public UserBL(UserRL userRL)
        {
            this.userRL = userRL;
        }
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
    }
}
