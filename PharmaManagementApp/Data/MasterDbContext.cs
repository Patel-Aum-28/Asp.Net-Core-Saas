using Microsoft.EntityFrameworkCore;
using PharmaManagementApp.Models;

namespace PharmaManagementApp.Data
{
    public class MasterDbContext : AppDbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var intialContent = new List<MasterTable>
            {
                new MasterTable{ PharmacyId = 1, PharmacyName = "Pharma-1", Email = "pharma1@mail.com", PasswordHash = "$2a$11$SNU6ITIfH5raJut0TwCkUeQdEqk9BDiVmABkj5GP3G.JL7V.skbsW", DbName = "ChildPharma1", IsActive = true},
                new MasterTable{ PharmacyId = 2, PharmacyName = "Pharma-2", Email = "pharma2@mail.com", PasswordHash = "$2a$11$T6Ovkdcntdx.lErzlZDn4ekJxUBqIWB0B/oIeGvNvdLAre.p2KY1y", DbName = "ChildPharma2", IsActive = true},
                new MasterTable{ PharmacyId = 3, PharmacyName = "Pharma-3", Email = "pharma3@mail.com", PasswordHash = "$2a$11$iuagNdx9.uQe7VizVbdG4.y5NmaVuMGFdXA1pcIhrhrZoaDsrnCVW", DbName = "ChildPharma3", IsActive = true}
            };

            modelBuilder.Entity<MasterTable>().HasData(intialContent);

        }

        public DbSet<MasterTable> MasterTable { get; set; }
    }
}
