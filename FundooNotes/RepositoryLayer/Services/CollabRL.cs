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
        /// <summary>
        /// method to add Collaborator
        /// </summary>
        /// <param name="collabaoratorModel"></param>
        /// <returns></returns>
        public Collaborator AddCollaborator(CollaboratorModel collabaoratorModel)
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
                    return collaborator;
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
        /// getting collaborators by Id
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <returns></returns>
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID)
        {
            try
            {
                var result = context.Collaborators.Where(x => x.Id == userID && x.NoteID == noteID).ToList();
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
        /// <summary>
        /// method to remove Collaborator
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <param name="collabEmail"></param>
        /// <returns></returns>
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail)
        {
            try
            {
                var collaborator = context.Collaborators.Where(x => x.CollabEmail == collabEmail && x.NoteID == noteID).FirstOrDefault();
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
