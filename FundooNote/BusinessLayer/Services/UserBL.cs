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
    using System.Collections.Generic;
    using System.Text;

    public class UserBL : IUserBL
    {

        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBL"/> class.
        /// </summary>
        /// <param name="userRL">The user rl.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="userRegist"></param>
        /// <returns>userinstance</returns>
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


        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>stringlogin</returns>
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


        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns>token</returns>
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>booleanvalue</returns>
        public bool ResetPassword(string Email, string Password, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(Email, Password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

