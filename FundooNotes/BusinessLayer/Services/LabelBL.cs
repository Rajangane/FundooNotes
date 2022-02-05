using BusinessLayer.Interfaces;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }
        public Labels CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
                return labelRL.CreateLabel(userID, noteID, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Labels> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return labelRL.RenameLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                return labelRL.RemoveLabel(userID, labelName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Labels> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                return labelRL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
