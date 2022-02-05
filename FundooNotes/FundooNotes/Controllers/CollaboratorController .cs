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
        public IActionResult AddCollaborator(long noteID, string collabEmail)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                CollaboratorModel collabaoratorModel = new CollaboratorModel();
                collabaoratorModel.Id = userId;
                collabaoratorModel.NoteId = noteID;
                collabaoratorModel.EmailId = collabEmail;
                var result = collaboratorBL.AddCollaborator(collabaoratorModel);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborator added successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access is denied" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetCollaboraterById")]
        public IEnumerable<Collaborator> GetCollaboratorsByID(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
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
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
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
