using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Tables;

namespace WebAPI
{
    public class BackupDatabase : DbContext
    {
        public DbSet<Tables.tbAdmins> Admins { get; set; }
        
        public DbSet<Tables.tbComputerConfig> ComputerConfig { get; set; }
        
        public DbSet<Tables.tbComputers> Computers { get; set; }

        public DbSet<Tables.tbConfigs> Configs { get; set; }

        public DbSet<Tables.tbDestinations> Destinations { get; set; }

        public DbSet<Tables.tbGroupConfig> GroupConfig { get; set; }

        public DbSet<Tables.tbGroups> Groups { get; set; }

        public DbSet<Tables.tbLogs> Logs { get; set; }

        public DbSet<Tables.tbMacAdresses> MacAdresses { get; set; }

        public DbSet<Tables.tbSnapshots> Snapshots { get; set; }

        public DbSet<Tables.tbSources> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b2_zakharchenkoartem_db1;user=zakharchenkoarte;password=123456;SslMode=none");
        }

    }
}
