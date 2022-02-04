using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class NotesController : ControllerBase
    {
        INotesBL noteBL;
        public NotesController(INotesBL NoteBL)
        {
            this.noteBL = NoteBL;
        }
       [Authorize]
        [HttpPost("AddNotes")]
        public IActionResult AddNote(NoteModel notes)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
               
                if (noteBL.AddNote(notes,userId))
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
        [Authorize]
        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(int noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

                if (noteBL.DeleteNotes(noteid))
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
        [Authorize]
        [HttpGet("GetAllNotesByID")]
        public IEnumerable<Notes> GetAllNotesOfUser(int UserId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            try
            {
                return noteBL.GetAllNotesOfUser(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(NoteModel notes, long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
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
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinORUnPinNote(long Noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
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
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveORUnarchiveNote(long noteid)
        {
            try
            {
               
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                if (noteBL.ArchiveORUnarchiveNote(userId,noteid))
                {
                    return this.Ok(new { Status = true, Message = "Archieve sucessfull" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Archieve unsucessfull" });
                }
            }


            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Trash")]
        public IActionResult TrashOrRestoreNote(long Noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
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
        [Authorize]
        [HttpPut]
        [Route("Image")]
        public IActionResult UploadImage(long noteId, IFormFile Image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                if (this.noteBL.UploadImage(noteId, Image))
                {
                    return this.Ok(new { Status = true, Message = "Upload Image Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Not Uploaded!" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message, InnerException = ex.InnerException });
            }
        }

    }

}