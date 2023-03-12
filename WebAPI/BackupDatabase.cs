using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Tables;

namespace WebAPI
{
    public class BackupDatabase : DbContext
    {
        public DbSet<Tables.Admins> Admins { get; set; }
        
        public DbSet<Tables.ComputerConfig> ComputerConfig { get; set; }
        
        public DbSet<Tables.Computers> Computers { get; set; }

        public DbSet<Tables.Configs> Configs { get; set; }

        public DbSet<Tables.Destinations> Destinations { get; set; }

        public DbSet<Tables.GroupConfig> GroupConfig { get; set; }

        public DbSet<Tables.Groups> Groups { get; set; }

        public DbSet<Tables.Logs> Logs { get; set; }

        public DbSet<Tables.MacAdresses> MacAdresses { get; set; }

        public DbSet<Tables.Snapshots> Snapshots { get; set; }

        public DbSet<Tables.Sources> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b2_zakharchenkoartem_db1;user=zakharchenkoarte;password=123456;SslMode=none");
        }

    }
}
