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
    public class NoteRL : INoteRL
    {
        private readonly contexts context1;
        private readonly IConfiguration _config;
        public NoteRL(contexts context, IConfiguration config)
        {
            this.context1 = context;
            _config = config;
        }
        public string ArchiveORUnarchiveNote(long noteid)
        {
            try
            {
                var Note = this.context1.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsArchive == true)
                {
                    Note.IsArchive = false;
                    this.context1.SaveChanges();
                    return "Note Unarchived";
                }
                else
                {
                    Note.IsArchive = true;
                    this.context1.SaveChanges();
                    return "Note Archived";
                }
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
                var ValidNote = this.context1.Notes.Where(Y => Y.NoteId == id).FirstOrDefault();
                this.context1.Notes.Remove(ValidNote);
                int result = this.context1.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddNote(NoteModel notes, int UserId)
        {
            try
            {
                Note newNotes = new Note();
                newNotes.Title = notes.Title;
                newNotes.Message = notes.Message;
                newNotes.Remainder = notes.Remainder;
                newNotes.Color = notes.Color;
                newNotes.Image = notes.Image;
                newNotes.IsArchive = notes.IsArchive;
                newNotes.IsPin = notes.IsPin;
                newNotes.IsTrash = notes.IsTrash;
                newNotes.Id = UserId;
                newNotes.Createat = notes.Createat;
                //Adding the data to database
                this.context1.Notes.Add(newNotes);
                //Save the changes in database
                int result = this.context1.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public IEnumerable<Note> GetAllNotesOfUser(int UserId)
        {
            return context1.Notes.Where(Y => Y.Id == UserId).ToList();
        }

        public string PinORUnPinNote(long noteid)
        {
            try
            {
                var Note = this.context1.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsPin == true)
                {
                    Note.IsPin = false;
                    this.context1.SaveChanges();
                    return "Note is UnPinned";
                }
                else
                {
                    Note.IsPin = true;
                    this.context1.SaveChanges();
                    return "Note is Pinned";
                }
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
                var Note = this.context1.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsTrash == true)
                {
                    Note.IsTrash = false;
                    this.context1.SaveChanges();
                    return "Note is Restored.";
                }
                else
                {
                    Note.IsTrash = true;
                    this.context1.SaveChanges();
                    return "Note is Trash";
                }
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
                var UpdateNote = this.context1.Notes.Where(Y => Y.NoteId == Noteid).FirstOrDefault();
                if (UpdateNote != null)
                {
                    UpdateNote.Title = notes.Title;
                    UpdateNote.Message = notes.Message;
                    UpdateNote.Remainder = notes.Remainder;
                    UpdateNote.Color = notes.Color;
                    UpdateNote.Image = notes.Image;
                    UpdateNote.IsArchive = notes.IsArchive;
                    UpdateNote.IsPin = notes.IsPin;
                    UpdateNote.IsTrash = notes.IsTrash;
                    UpdateNote.Createat = notes.Createat;
                }
                var result = this.context1.SaveChanges();
                if (result > 0)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
