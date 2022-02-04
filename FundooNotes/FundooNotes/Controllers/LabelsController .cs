using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : Controller
    {
        ILabelBL labelBL;
        public LabelsController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [Authorize]
        [HttpPost("AddLabel")]
        public IActionResult AddLabel(string labelName, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.CreateLabel(userID, noteID, labelName))
                {
                    return this.Ok(new { success = true, message = "Label added successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Label already created" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpPut("RenameLabel")]
        public IActionResult RenameLabel(string lableName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.RenameLabel(userID, lableName, newLabelName))
                {
                    return this.Ok(new { success = true, message = "Label renamed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [Authorize]
        [HttpDelete("RemoveLabel")]
        public IActionResult RemoveLabel(string lableName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (labelBL.RemoveLabel(userID, lableName))
                {
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("GetLabelsByNoteId")]
        public IEnumerable GetLabelsByNoteID(long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return labelBL.GetLabelsByNoteID(userID, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
