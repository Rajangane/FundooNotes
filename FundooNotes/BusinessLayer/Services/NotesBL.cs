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

        public Notes AddNote(NoteModel notesmodel, long UserId)
        {
            try
            {
                return NoteRL.AddNote(notesmodel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public Notes ArchiveORUnarchiveNote(long userId, long noteID)
        {
            try
            {
                return NoteRL.ArchiveORUnarchiveNote(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes ColorNote(long userId, long noteID, string color)
        {
            try
            {
                return NoteRL.ColorNote(userId, noteID, color);
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

        public IEnumerable<Notes> GetAllNotesByUserId(int UserId)
        {
            try
            {
                return NoteRL.GetAllNotesByUserId(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return NoteRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Notes PinORUnPinNote(long userId, long noteID)
        {
            try
            {
                return NoteRL.PinORUnPinNote(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Notes TrashOrRestoreNote(long userId, long noteID)
        {
            try
            {
                return NoteRL.TrashOrRestoreNote(userId, noteID);
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
        public Notes UploadImage(long noteId, IFormFile image)
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
        public IEnumerable<Notes> GetAllNotesUsingRedisCache()
        {
            try
            {
                return NoteRL.GetAllNotesUsingRedisCache();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
