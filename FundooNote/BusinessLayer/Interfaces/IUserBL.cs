using CommonLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
   public interface IUserBL
    {
        public User Registration(UserRegistration userRegist);
    }
}
