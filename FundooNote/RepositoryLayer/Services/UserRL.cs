using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        //instance of  FundooContext Class
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _appSettings;
        //Constructor
        public UserRL(FundooContext fundooContext,IConfiguration _appSettings)
        {
            this.fundooContext = fundooContext;
            this._appSettings = _appSettings;
        }
        //Method to register User Details.
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegist.FirstName;
                newUser.LastName = userRegist.LastName;
                newUser.Email = userRegist.Email;
                newUser.Password = userRegist.Password;
                fundooContext.UserTable.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Validating Email And Password
        public string Login(string email, string password)
        {
            try
            {
                //if Email and password is empty return null. 
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                //Linq query matches given input in database and returns that entry from the database.
                var result = fundooContext.UserTable.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                var id = result.Id;
                if (result != null)
                    //Calling Jwt Token Creation method and returning token.
                    return GenerateSecurityToken(result.Email, id);
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Implementing Jwt Token For Login using Email and Id
        private string GenerateSecurityToken(string Email, long Id)
        {
            //header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //payload
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())

        };
            //signature
            var token = new JwtSecurityToken(_appSettings["Jwt:Issuer"],
              _appSettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        // Adding forgot PAsswordmethod
        public string ForgotPassword(string email)
        {
            try
            {
                var existingEmail = this.fundooContext.UserTable.Where(e => e.Email == email).FirstOrDefault();
                if (existingEmail != null)
                {
                    var token = GenerateSecurityToken(existingEmail.Email, existingEmail.Id);
                    new MsMqModel().Sender(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string Email,string Password,string confirmPassword)
        {
            try
            {
                if (Password.Equals(confirmPassword))
                {
                    var user = fundooContext.UserTable.Where(e => e.Email == Email).FirstOrDefault();
                    user.Password = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        } 
        public Notes CreateNotes(UserNotesData notesCreate,long userId)
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
                        return newNotes;
                    else
                        return null;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes UpdateNotes(UserNotesData noteUpdate, long noteId,long userId)
        {
            try
            {
               Notes newNotes = new Notes();
                // newNotes.Id = userId;
                var result = fundooContext.NotesTable.Where(e => e.NotesId == noteId);
                if (result.Title = noteUpdate.Title)
                {
                    fundooContext.NotesTable.Update(newNotes);
                    fundooContext.SaveChanges();
                    return newNotes;
                }
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
    

