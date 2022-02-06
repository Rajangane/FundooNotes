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
        /// <summary>
        /// method to archive or Unarchive the notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public Notes ArchiveORUnarchiveNote(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checkarch = note.IsArchive;
                    if (checkarch == true)
                    {
                        note.IsArchive = false;
                    }
                    if (checkarch == false)
                    {
                        note.IsArchive = true;
                    }
                    context.SaveChanges();
                    return note;
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
        /// method to color the note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Notes ColorNote(long userId, long noteID, string color)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    note.Color = color;
                    context.SaveChanges();
                    return note;
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
        /// method to get all notes using Redis cache
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notes> GetAllNotesUsingRedisCache()
        {
            return context.Notes.ToList();
        }
        /// <summary>
        /// method to delete the note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
        /// <summary>
        /// method to add the note
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public Notes AddNote(NoteModel notesmodel, long UserId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.Id = UserId;
                newNotes.Title = notesmodel.Title;
                newNotes.Message = notesmodel.Message;
                newNotes.Remainder = notesmodel.Remainder;
                newNotes.Color = notesmodel.Color;
                newNotes.Image = notesmodel.Image;
                newNotes.IsArchive = notesmodel.IsArchive;
                newNotes.IsPin = notesmodel.IsPin;
                newNotes.IsTrash = notesmodel.IsTrash;
                newNotes.Createat = notesmodel.Createat;
                //Adding the data to database
                this.context.Notes.Add(newNotes);
                //Save the changes in database
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return newNotes;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        /// <summary>
        /// method to get all notes by userId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<Notes> GetAllNotesByUserId(int UserId)
        {
            return context.Notes.Where(Y => Y.Id == UserId).ToList();
        }

        public IEnumerable<Notes> GetAllNotes()
        {
            return context.Notes.ToList();
        }
        public Notes PinORUnPinNote(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(x => x.Id == userId && x.NoteId == noteID);
                if (note != null)
                {
                    bool checkpin = note.IsPin;
                    if (checkpin == true)
                    {
                        note.IsPin = false;
                    }
                    if (checkpin == false)
                    {
                        note.IsPin = true;
                    }
                    context.SaveChanges();
                    return note;
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
        /// method Trash or Restore the note
        /// </summary>
        /// <param name="noteid"></param>
        /// <returns></returns>
        public Notes TrashOrRestoreNote(long userId, long noteID)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checktrash = note.IsTrash;
                    if (checktrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (checktrash == false)
                    {
                        note.IsTrash = true;
                    }
                    context.SaveChanges();
                    return note;
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
        /// method to Update a note
        /// </summary>
        /// <param name="notes"></param>
        /// <param name="Noteid"></param>
        /// <returns></returns>
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
        /// <summary>
        /// method to upload the Image
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Notes UploadImage(long noteId, IFormFile image)
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
                    return notes;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
