using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
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
    public class NotesRL : INotesRL
    {
        private readonly Context context;
        private readonly IConfiguration _config;
        public NotesRL(Context context, IConfiguration config)
        {
            this.context = context;
            _config = config;
        }
        public bool ArchiveORUnarchiveNote(long UserId, long noteid)
        {
            try
            {
                var Note = this.context.Notes.FirstOrDefault(x => x.Id == UserId && x.NoteId == noteid);
                if (Note.IsArchive == true)
                {
                   Note.IsArchive = false;
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    Note.IsArchive = true;
                    this.context.SaveChanges();
                    return false;
                }
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
                var Note = this.context.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (Note.Color != color)
                {
                    Note.Color = color;
                    this.context.SaveChanges();
                    return "Note color is changed.";
                }
                else
                {
                    Note.IsTrash = true;
                    this.context.SaveChanges();
                    return "choose different color";
                }
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
                var ValidNote = this.context.Notes.Where(Y => Y.NoteId == id).FirstOrDefault();
                this.context.Notes.Remove(ValidNote);
                int result = this.context.SaveChanges();
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

        public bool AddNote(NoteModel notes, long UserId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.Id = UserId;
                newNotes.Title = notes.Title;
                newNotes.Message = notes.Message;
                newNotes.Remainder = notes.Remainder;
                newNotes.Color = notes.Color;
                newNotes.Image = notes.Image;
                newNotes.IsArchive = notes.IsArchive;
                newNotes.IsPin = notes.IsPin;
                newNotes.IsTrash = notes.IsTrash;
                newNotes.Createat = notes.Createat;
                //Adding the data to database
                this.context.Notes.Add(newNotes);
                //Save the changes in database
                int result = this.context.SaveChanges();
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
        public IEnumerable<Notes> GetAllNotesOfUser(int UserId)
        {
            return context.Notes.Where(Y => Y.Id == UserId).ToList();
        }

        public string PinORUnPinNote(long noteid)
        {
            try
            {
                var Note = this.context.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsPin == true)
                {
                    Note.IsPin = false;
                    this.context.SaveChanges();
                    return "Note is UnPinned";
                }
                else
                {
                    Note.IsPin = true;
                    this.context.SaveChanges();
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
                var Note = this.context.Notes.FirstOrDefault(x => x.NoteId == noteid);
                if (Note.IsTrash == true)
                {
                    Note.IsTrash = false;
                    this.context.SaveChanges();
                    return "Note is Restored.";
                }
                else
                {
                    Note.IsTrash = true;
                    this.context.SaveChanges();
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
                var UpdateNote = this.context.Notes.Where(Y => Y.NoteId == Noteid).FirstOrDefault();
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
                var result = this.context.SaveChanges();
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

        public bool UploadImage(long noteId, IFormFile image)
        {
            try
            {
                var notes = this.context.Notes.FirstOrDefault(x => x.NoteId == noteId);
                if (notes != null)
                {
                    Account account = new Account
                    (
                    _config["CloudinaryAccount:CloudName"],
                    _config["CloudinaryAccount:ApiKey"],
                    _config["CloudinaryAccount:ApiSecret"]
                    );
                    var path = image.OpenReadStream();
                    Cloudinary cloudinary = new Cloudinary(account);
                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, path)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    context.Notes.Attach(notes);
                    notes.Image = uploadResult.Uri.ToString();
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
