using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Services;


namespace BusinessLayer.Services
{
    public class CollaabBL : ICollabBL
    {
        private readonly ICollabRL collabratorRL;
        public CollaabBL(ICollabRL collabratorRL)
        {
            this.collabratorRL = collabratorRL;
        }
        //add collab
        public Collaborator AddCollab(string email, long userId, long noteId)
        {
            try
            {
                return collabratorRL.AddCollab(email, userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Remove Collab
        public bool DeleteCollab(long userId, long collabid)
        {
            try
            {
                return collabratorRL.DeleteCollab(userId, collabid);
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
                return collabratorRL.GetAllCollabs(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


