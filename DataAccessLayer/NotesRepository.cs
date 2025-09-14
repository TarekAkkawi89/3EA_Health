using _3EA_Health.Data;
using _3EA_Health.Entities;
using _3EAHealth.DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3EAHealth.DataAccessLayer
{
    public class NotesRepository : INotesRepository
    {
        private readonly Datacontext _context;

        public NotesRepository(Datacontext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Notes>> AddANote(Notes note)
        {
           
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note; // just return the saved entity
        }


        public async Task<Notes> DeleteNoteById(int id, int tenantId)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.tenantId == tenantId);

            if (note == null)
                return null;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return note;
        }


        public async Task<ActionResult<Notes>> GetByPatientId(int patientId, int tenantId)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.patientId == patientId && n.tenantId == tenantId);
            return note;
        }


        public async Task<Notes> UpdateNoteById(int id, Notes notes, int tenantId)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.tenantId == tenantId);

            if (existingNote == null)
                return null;

            // Update only the fields you allow clients to modify
            existingNote.author = notes.author;
            existingNote.text = notes.text;
            existingNote.patientId = notes.patientId;
            existingNote.tenantId = notes.tenantId;

            await _context.SaveChangesAsync();
            return existingNote;
        }
    }
}
