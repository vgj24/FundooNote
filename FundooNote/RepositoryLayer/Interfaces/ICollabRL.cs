using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;


namespace RepositoryLayer.Interfaces
{
   public interface ICollabRL
    {
        public Collaborator AddCollab(string email, long userId, long noteId);
        public bool DeleteCollab(long userId, long collabid);
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId);

    }
}
