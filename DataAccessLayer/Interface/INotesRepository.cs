using _3EA_Health.Entities;
using Microsoft.AspNetCore.Mvc;

namespace _3EAHealth.DataAccessLayer.Interface
{
    public interface INotesRepository
    {
        Task<ActionResult<Notes>> AddANote(Notes note);
        Task<ActionResult<Notes>> GetByPatientId(int patientId, int tenantId);
        Task<Notes> UpdateNoteById(int id, Notes notes, int tenantId);
        Task<Notes> DeleteNoteById(int id, int tenantId);
        
    }
}
