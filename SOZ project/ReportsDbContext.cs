using Microsoft.EntityFrameworkCore;
using SOZ_project.Models;

namespace SOZ_project
{
    public class ReportsDbContext : DbContext
    {
        private string _connectionstring = "Server=127.0.0.1,1433;Database=sozDb;User ID=sa;Password=wwsi2022S@;Encrypt=False;";

        public DbSet<ReportModel> Reports { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionstring);
        }
    }
}
