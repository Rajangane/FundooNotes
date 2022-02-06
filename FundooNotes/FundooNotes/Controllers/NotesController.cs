using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.AppContexts;
using RepositoryLayer.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly Context context;
        private readonly IDistributedCache distributedCache;
        INotesBL noteBL;
        public NotesController(INotesBL NoteBL, IMemoryCache memoryCache, Context context, IDistributedCache distributedCache)
        {
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
            this.noteBL = NoteBL;
        }
       [Authorize]
        [HttpPost("AddNotes")]
        public IActionResult AddNote(NoteModel notesmodel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = noteBL.AddNote(notesmodel, userId);
                if(result != null)
                //if (noteBL.AddNote(notes,userId))

                {
                    return this.Ok(new { Success = true, message = "Note Added" , Response = result});
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
        [HttpGet("GetAllNotesByUserID")]
        public IEnumerable<Notes> GetAllNotesByUserId(int UserId)
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            try
            {
                return noteBL.GetAllNotesByUserId(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetAllNotes")]
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return noteBL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<Notes>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<Notes>>(serializedNotesList);
            }
            else
            {
                NotesList = (List<Notes>)noteBL.GetAllNotes();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
            }
            return Ok(NotesList);
        }
        [Authorize]
        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(NoteModel notes, long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = noteBL.UpdateNotes(notes, NoteId);
                //NoteModel response = noteBL.UpdateNotes(notes,NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Updating a note Sucessfull", Response = result });
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
        [HttpPut("ColoreNote")]
        public IActionResult ColorNotes(long noteID, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = noteBL.ColorNote(userId, noteID, color);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult PinNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = noteBL.PinORUnPinNote(userId, noteID);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("ArchiveOrUnArchiveNote")]
        public IActionResult ArchieveNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = noteBL.ArchiveORUnarchiveNote(userId, noteID);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut("Trash")]
        public IActionResult TrashNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = noteBL.TrashOrRestoreNote(userId, noteID);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
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
                //if (this.noteBL.UploadImage(noteId, Image))
                var result = noteBL.UploadImage(noteId, Image);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Upload Image Successfully" ,Response = result});
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