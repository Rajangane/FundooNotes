using CommonLayer.Models;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public Collaborator AddCollaborator(CollaboratorModel collabaoratorModel);
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID);
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail);
    }
}
