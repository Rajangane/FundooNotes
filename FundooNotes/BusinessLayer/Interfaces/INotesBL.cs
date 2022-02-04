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

        public bool AddNote(NoteModel notes, long UserId);


        NoteModel UpdateNotes(NoteModel notes, long Noteid);
        bool DeleteNotes(int id);
        string PinORUnPinNote(long noteid);
        string TrashOrRestoreNote(long noteid);
        string ColorNote(long noteId, string color);
        bool ArchiveORUnarchiveNote(long userId, long noteid);
        IEnumerable<Notes> GetAllNotesOfUser(int UserId);
        public bool UploadImage(long noteId, IFormFile image);

    }
}
