using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL NoteRL;
        public NoteBL(INoteRL NoteRL)
        {
            this.NoteRL = NoteRL;
        }

        public bool AddNote(NoteModel notes, int UserId)
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

        public string ArchiveORUnarchiveNote(long noteid)
        {
            try
            {
                return this.NoteRL.ArchiveORUnarchiveNote(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ColorNote(long noteId, string color)
        {
            throw new NotImplementedException();
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

        public IEnumerable<Note> GetAllNotesOfUser(int UserId)
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
    }
}
