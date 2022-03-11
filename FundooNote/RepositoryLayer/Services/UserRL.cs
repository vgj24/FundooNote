//-----------------------------------------------------------------------
// <copyright file="UserRL.cs" company="Vrushali">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using System;
    using CommonLayer.Model;
    using global::RepositoryLayer.Context;
    using global::RepositoryLayer.Entity;
    using global::RepositoryLayer.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;


    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// instance of  FundooContext Class
        /// </summary>
        /// <param name="fundooContext">The fundoo context.</param>
        /// <param name="_appSettings">The application settings.</param>
        public UserRL(FundooContext fundooContext, IConfiguration _appSettings)
        {
            this.fundooContext = fundooContext;
            this._appSettings = _appSettings;
        }

        /// <summary>
        /// Registrations the specified user regist.
        /// </summary>
        /// <param name="userRegist">The user regist.</param>
        /// <returns>SecurityToken</returns>
        public User Registration(UserRegistration userRegist)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegist.FirstName;
                newUser.LastName = userRegist.LastName;
                newUser.Email = userRegist.Email;
                newUser.Password = userRegist.Password;
                var encrypt = Encrypt(newUser.Password);
                newUser.Password = encrypt;
                fundooContext.UserTable.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
                else
                { 
                    return null; 
                }
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
        /// <returns>Login</returns>
        public string Login(string email, string password)
        {
            try
            {
                ////if Email and password is empty return null. 
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                ////Linq query matches given input in database and returns that entry from the database.
                //var emailcheck = fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                //var decryptPass = Decrypt(emailcheck.Password);
                //if (decryptPass == password)
                //{
                //    if (emailcheck != null)
                //    {
                //        return GenerateSecurityToken(emailcheck.Email, emailcheck.Id);
                //    }
                //    else
                //        return null;
                //}
                //else
                //{
                //    return null;
                //}
                var result = this.fundooContext.UserTable.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                var id = result.Id;
                if (result != null)
                {
                    return GenerateSecurityToken(result.Email, id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Token</returns>
        private string GenerateSecurityToken(string Email, long Id)
        {
            ////header--coosist two part 1. type of token:JWT and algorithm used  HMAC SHA256 or RSA.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            ////payload --contains claims --claims are 3 types 
            var claims = new[] 
            {new Claim(ClaimTypes.Email,Email),
             new Claim("Id",Id.ToString())};
            ////signature
            var token = new JwtSecurityToken(_appSettings["Jwt:Issuer"],
              _appSettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Token</returns>
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
                {
                    return null;
                }
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
        /// <returns>Passoword</returns>
        public bool ResetPassword(string Email, string Password, string confirmPassword)
        {
            try
            {
                if (Password.Equals(confirmPassword))
                {
                    var user = fundooContext.UserTable.Where(e => e.Email == Email).FirstOrDefault();
                    user.Password = confirmPassword;
                    user.Password = Encrypt(user.Password);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Encrypts the specified cleartext.
        /// </summary>
        /// <param name="Cleartext">The cleartext.</param>
        /// <returns>Encrypted password</returns>
        public string Encrypt(string Cleartext)
        {
            try
            {
                
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(Cleartext);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        Cleartext = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return Cleartext;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>DecrptPassword</returns>
        public string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registrations the specified user regist.
        /// </summary>
        /// <param name="userRegist">The user regist.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        Entity.User IUserRL.Registration(UserRegistration userRegist)
        {
            throw new NotImplementedException();
        }
    }
}


