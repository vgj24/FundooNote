//-----------------------------------------------------------------------
// <copyright file="IUserRL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interfaces
{
    using System;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Entity;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserRL
    {
        /// <summary>
        /// Registrations the specified user regist.
        /// </summary>
        /// <param name="userRegist">The user regist.</param>
        /// <returns>userinstance</returns>
        public User Registration(UserRegistration userRegist);

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>login</returns>
        public string Login(string email, string password);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns>token</returns>
        public string ForgotPassword(string Email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>password</returns>
        public bool ResetPassword(string Email, string Password, string confirmPassword);
    }
}
