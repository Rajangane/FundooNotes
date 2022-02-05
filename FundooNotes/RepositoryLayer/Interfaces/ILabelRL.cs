using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public Labels CreateLabel(long userID, long noteID, string labelName);
        public IEnumerable<Labels> RenameLabel(long userID, string oldLabelName, string labelName);
        public bool RemoveLabel(long userID, string labelName);
        public IEnumerable<Labels> GetLabelsByNoteID(long userID, long noteID);

    }
}
