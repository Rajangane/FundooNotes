using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        INotesRL NoteRL;
        public NotesBL(INotesRL NoteRL)
        {
            this.NoteRL = NoteRL;
        }

        public bool AddNote(NoteModel notes, long UserId)
        {
            try
            {
                return NoteRL.AddNote(notes, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public bool ArchiveORUnarchiveNote(long userId, long noteid)
        {
            try
            {
                return this.NoteRL.ArchiveORUnarchiveNote(userId,noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ColorNote(long noteId, string color)
        {
            try
            {
                return this.NoteRL.ColorNote(noteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotes(int id)
        {
            try
            {
                return NoteRL.DeleteNotes(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Notes> GetAllNotesOfUser(int UserId)
        {
            try
            {
                return NoteRL.GetAllNotesOfUser(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

   
        public string PinORUnPinNote(long noteid)
        {
            try
            {
                return this.NoteRL.PinORUnPinNote(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string TrashOrRestoreNote(long noteid)
        {
            try
            {
                return this.NoteRL.TrashOrRestoreNote(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteModel UpdateNotes(NoteModel notes, long Noteid)
        {
            try
            {
                return NoteRL.UpdateNotes(notes, Noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UploadImage(long noteId, IFormFile image)
        {
            try
            {
                return this.NoteRL.UploadImage(noteId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
