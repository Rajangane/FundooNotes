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
    public class LabelRL : ILabelRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public LabelRL(Context context, IConfiguration config)
        {
            this.context = context;
            this.configuration = config;
        }
        /// <summary>
        /// method to create the label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public Labels CreateLabel(long userID, long noteID, string labelName)
        {
            try
            {
             
                Labels labels = new Labels();
                var lab = context.Labels.Where(x => x.LabelName == labelName).FirstOrDefault();
                int result = context.SaveChanges();
                if (lab == null)
                {
                    labels.LabelName = labelName;
                    labels.NoteID = noteID;
                    labels.Id = userID;
                    context.Labels.Add(labels);
                    context.SaveChanges();
                    return labels;
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
        /// method to rename the label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldLabelName"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public IEnumerable<Labels> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(x => x.Id == userID && x.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    label.LabelName = labelName;
                }
                context.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// method to remove the label
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="labelName"></param>
        /// <returns></returns>
        public bool RemoveLabel(long userID, string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(x => x.Id == userID && x.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    context.Labels.Remove(label);
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// getting all labels by noteId
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="noteID"></param>
        /// <returns></returns>
        public IEnumerable<Labels> GetLabelsByNoteID(long userID, long noteID)
        {
            try
            {
                var result = context.Labels.Where(x => x.NoteID == noteID && x.Id == userID).ToList();
                if (result != null)
                {
                    return result;
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
    }
}
