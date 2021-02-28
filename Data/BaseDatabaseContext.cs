using Base_API_Structure.Models;
using Microsoft.EntityFrameworkCore;

namespace Base_API_Structure.Data
{
    public class BaseDatabaseContext : DbContext
    {
        
        public BaseDatabaseContext(DbContextOptions<BaseDatabaseContext> opt) : base(opt)
        {
            
        }

        public DbSet<BaseModel> BaseModels { get; set; }

    }

}