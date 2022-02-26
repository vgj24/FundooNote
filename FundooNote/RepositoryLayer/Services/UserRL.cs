using CommonLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
  public class UserRL :IUserRL
    {
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newuser = new User();
                newuser.FirstName = userRegist.FirstName;
                newuser.LastName = userRegist.LastName;
                newuser.Email = userRegist.Email;
                newuser.Password = userRegist.Password;
                fundooContext.UserTable.Add(newuser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newuser;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
