using CommonLayer.Models;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
   public interface INoteRL
    {

        public bool AddNote(NoteModel notes, int UserId);
        NoteModel UpdateNotes(NoteModel notes,  long Noteid);
        bool DeleteNotes(int id);
        string PinORUnPinNote(long noteid);
        string TrashOrRestoreNote(long noteid);
        string ColorNote(long noteId, string color);
        string ArchiveORUnarchiveNote(long noteid);
        IEnumerable<Note> GetAllNotesOfUser(int UserId);

    }
}
    
