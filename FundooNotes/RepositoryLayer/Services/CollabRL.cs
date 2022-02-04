using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContexts;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        private readonly Context context;
        private readonly IConfiguration _config;
        public CollabRL(Context context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }
        public bool AddCollaborator(CollaboratorModel collabaoratorModel)
        {
            try
            {
                Collaborator collaborator = new Collaborator();
                Notes notes = context.Notes.Where(x => x.NoteId == collabaoratorModel.NoteId && x.Id == collabaoratorModel.Id).FirstOrDefault();
                if (notes != null)
                {
                    collaborator.NoteID = collabaoratorModel.NoteId;
                    collaborator.CollabEmail = collabaoratorModel.EmailId;
                    collaborator.Id = collabaoratorModel.Id;
                    context.Collaborators.Add(collaborator);
                    var result = context.SaveChanges();
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
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID)
        {
            try
            {
                var result = context.Collaborators.Where(e => e.Id == userID && e.NoteID == noteID).ToList();
                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail)
        {
            try
            {
                var collaborator = context.Collaborators.Where(e => e.CollabEmail == collabEmail && e.NoteID == noteID).FirstOrDefault();
                if (collaborator != null)
                {
                    context.Collaborators.Remove(collaborator);
                    context.SaveChanges();
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
    }
}
