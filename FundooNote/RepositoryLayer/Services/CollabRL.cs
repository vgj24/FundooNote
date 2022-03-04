using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _appSettings;

        public CollabRL(FundooContext fundooContext, IConfiguration _appSettings)
        {
            this.fundooContext = fundooContext;
            this._appSettings = _appSettings;
        }
        //Add Collboration
        public Collaborator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(e => e.Email == email);

                if (result.Email == email)
                {
                    Collaborator newcollab = new Collaborator();
                    newcollab.CollabEmail = email;
                    newcollab.Id = userId;
                    newcollab.NotesId = noteId;
                    fundooContext.CollboratorTable.Add(newcollab);
                    fundooContext.SaveChanges();
                    return newcollab;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteCollab(long userId, long collabid)
        {
            try
            {
                var result = fundooContext.CollboratorTable.Where(e => e.Id == userId && e.CollabId == collabid).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.CollboratorTable.Remove(result);
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
        public IEnumerable<Collaborator> GetAllCollabs(long userId, long noteId)
        {
            try
            {
                var result = fundooContext.CollboratorTable.Where(e => e.NotesId == noteId).ToList();
                if (result != null)
                {
                    return result;
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

