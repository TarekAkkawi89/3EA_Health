using _3EA_Health.Data;
using _3EA_Health.Entities;
using _3EAHealth.DataAccessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3EAHealth.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        const string API_KEY = "3EAHealth";
        private readonly INotesRepository _repo;

        public NotesController(INotesRepository repo)
        {
            _repo = repo;
        }


        [HttpPost]
        public async Task<ActionResult<Notes>> PostNotes(Notes note)
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization") || Request.Headers["Authorization"].ToString() != API_KEY)
                {
                    return Unauthorized(new { Message = "Unauthorized Access" });
                }

                var notes = await _repo.AddANote(note);


                // If something failed and null came back
                if (note == null || notes.Value == null)
                {
                    return BadRequest(new { Message = "Note could not be created." }); // status 400 because it was unable to save data
                }

                // Return 201 Created with a location header
                return Ok();
            }
            catch
            {
                //Status 500 internal error
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the note." });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotes(int id, [FromHeader(Name = "X-Tenant-Id")] int tenantId)
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization") || Request.Headers["Authorization"].ToString() != API_KEY)
                {
                    //status 401
                    return Unauthorized(new { Message = "Unauthorized Access" });
                }

                if (tenantId <=0)
                {
                    //Status 400
                    return BadRequest(new { Message = "X-Tenant-Id header is required and must be an integer" });
                }

                

                var notes = await _repo.DeleteNoteById(id, tenantId);
                if (notes == null)
                {
                    //Status 404
                    return NotFound(new { Message = "Note was not found to be deleted." }); //status 404 note has not been found for deletion
                }

                if (notes.tenantId != tenantId)
                    return Forbid(); //403 . It should never come here but safety check

                return NoContent(); // status 204 success but client does not need any body (JSON)
            }
            catch
            {
                //Status 500 internal error
                return StatusCode(500, new { Message = "An unexpected error occurred while deleting the note." });
            }
            
        }

        [HttpGet("{patientid}")]
        public async Task<ActionResult<Notes>> GetNotes(int patientid, [FromHeader(Name = "X-Tenant-Id")] int tenantId)
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization") || Request.Headers["Authorization"].ToString() != API_KEY)
                {
                    //Status 401
                    return Unauthorized(new { Message = "Unauthorized Access" });
                }

                if (tenantId <= 0)
                {
                    //Status 400
                    return BadRequest(new { Message = "X-Tenant-Id header is required and must be an integer" });
                }

                var note = await _repo.GetByPatientId(patientid, tenantId);

                if (note == null || note.Value == null)
                {
                    //Status 404
                    return NotFound(new { Message = $"No note found for patientId {patientid}." });
                }

                if (note.Value.tenantId != tenantId)
                    return Forbid(); //403 . It should never come here but safety check

                return Ok(note); //Status 200
            }
            catch
            {
                //Status 500 internal error
                return StatusCode(500, new { Message = "An unexpected error occurred while retrieving the note." });
            }
         
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes(int id, Notes notes, [FromHeader(Name = "X-Tenant-Id")] int tenantId)
        {
            try
            {
                if (!Request.Headers.ContainsKey("Authorization") || Request.Headers["Authorization"].ToString() != API_KEY)
                {
                    //Status 401
                    return Unauthorized(new { Message = "Unauthorized Access" });
                }

                if (tenantId <= 0 )
                {
                    //Status 400
                    return BadRequest(new { Message = "X-Tenant-Id header is required and must be an integer" });
                }

                if (id != notes.Id)
                {
                    //Status 400
                    return BadRequest(new { Message = "ID in URL does not match ID in body." });
                }

                var updatedNote = await _repo.UpdateNoteById(id, notes, tenantId);

                if (updatedNote == null)
                {
                    //Status 404
                    return NotFound(new { Message = $"Note with id {id} not found." }); 
                }

                if (updatedNote.tenantId != tenantId)
                    return Forbid(); //403 . It should never come here but safety check

                return NoContent();// status 204 success but client does not need any body (JSON)
            }
            catch
            {
                //Status 500 internal error
                return StatusCode(500, new { Message = "An unexpected error occurred while updating the note." });
            }
            

        }

       
    }
}
