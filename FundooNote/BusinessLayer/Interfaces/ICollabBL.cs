using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
   public interface ICollabBL
    {
        public Collaborator AddCollab(string email, long userId, long noteId);
        public bool DeleteCollab(long userId, long collabid);
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId);
    }
}
