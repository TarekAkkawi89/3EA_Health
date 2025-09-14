using _3EA_Health.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3EA_Health.Data
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) :base(options)
        {
            
        }    

        public DbSet<Notes> Notes => Set<Notes>();
    }
}
