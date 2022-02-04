using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collaboratorRL;

        public CollabBL(ICollabRL collaboratorRL)
        {
            this.collaboratorRL = collaboratorRL;
        }
        public bool AddCollaborator(CollaboratorModel collabaoratorModel)
        {
            try
            {
                return collaboratorRL.AddCollaborator(collabaoratorModel);
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
                return collaboratorRL.GetCollaboratorsByID(userID, noteID);
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
                return collaboratorRL.RemoveCollaborator(userID, noteID, collabEmail);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
