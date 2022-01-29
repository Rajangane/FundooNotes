using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        public NoteController(INoteBL NoteBL)
        {
            this.noteBL = NoteBL;

        }
        [HttpPost("AddNotes")]
        public IActionResult AddNote(NoteModel notes, int UserId)
        {
            try
            {
                if (noteBL.AddNote(notes, UserId))
                {
                    return this.Ok(new { Success = true, message = "Note Added" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Add note" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(int id)
        {
            try
            {
                if (noteBL.DeleteNotes(id))
                {
                    return this.Ok(new { Success = true, message = "Note Deleted  Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete the note" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllNotes")]
        public IEnumerable<Note> GetAllNotesOfUser(int UserId)
        { 
            
            try
            {
                return noteBL.GetAllNotesOfUser(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(NoteModel notes, long NoteId)
        {
            try
            {
                NoteModel response = noteBL.UpdateNotes(notes,NoteId);
                if (response != null)
                {
                    return this.Ok(new { Success = true, message = "Updating a note Sucessfull" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Update a note" });
                }
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult PinORUnPinNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.PinORUnPinNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveORUnarchiveNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.ArchiveORUnarchiveNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [HttpDelete]
        [Route("Trash")]
        public IActionResult TrashOrRestoreNote(long Noteid)
        {
            try
            {
                var result = this.noteBL.TrashOrRestoreNote(Noteid);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = result });
                }

                return this.BadRequest(new { Status = false, Message = result });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
    }

}