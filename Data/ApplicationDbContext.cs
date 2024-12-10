using CourtComplaintFormBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CourtComplaintFormBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ContactFormData> ContactForms { get; set; }
    }
}
