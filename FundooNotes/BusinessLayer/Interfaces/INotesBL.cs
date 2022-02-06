using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INotesBL
    {

        public Notes AddNote(NoteModel notesmodel, long UserId);
        NoteModel UpdateNotes(NoteModel notes, long Noteid);
        public Notes ColorNote(long userId, long noteID, string color);
        bool DeleteNotes(int id);
        public Notes PinORUnPinNote(long userId, long noteID);
        public Notes TrashOrRestoreNote(long userId, long noteID);
        public Notes ArchiveORUnarchiveNote(long userId, long noteID);
        public IEnumerable<Notes> GetAllNotesByUserId(int UserId);
        public IEnumerable<Notes> GetAllNotes();
        public Notes UploadImage(long noteId, IFormFile image);
        public IEnumerable<Notes> GetAllNotesUsingRedisCache();

    }
}
