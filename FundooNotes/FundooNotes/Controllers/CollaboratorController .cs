using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : Controller
    {
        ICollabBL collaboratorBL;

        public CollaboratorController (ICollabBL collaboratorBL)
        {
            this.collaboratorBL = collaboratorBL;
        }
        [Authorize]
        [HttpPost("AddCollaborater")]
        public IActionResult AddCollaborator(long noteID, string collabEmail, long userId)
        {
            try
            {
                userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                CollaboratorModel collabaoratorModel = new CollaboratorModel();
                collabaoratorModel.Id = userId;
                collabaoratorModel.NoteId = noteID;
                collabaoratorModel.EmailId = collabEmail;
                if (collaboratorBL.AddCollaborator(collabaoratorModel))
                {
                    return this.Ok(new { Success = true, message = "Collaborator Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "you dont have permission" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
        [Authorize]
        [HttpGet("GetCollaboraterById")]
        public IEnumerable<Collaborator> GetCollaboratorsByID(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return collaboratorBL.GetCollaboratorsByID(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete("RemoveCollaborater")]
        public IActionResult RemoveCollaborator(long noteID, string collaboratorEmail)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (collaboratorBL.RemoveCollaborator(userId, noteID, collaboratorEmail))
                {
                    return this.Ok(new { success = "true", message = "Collaborator removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = "false", message = "User Access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
