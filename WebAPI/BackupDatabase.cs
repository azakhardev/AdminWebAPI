using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Tables;

namespace WebAPI
{
    public class BackupDatabase : DbContext
    {
        public DbSet<tbAdmins> Admins { get; set; }
        
        public DbSet<tbComputerConfig> ComputerConfig { get; set; }
        
        public DbSet<tbComputers> Computers { get; set; }

        public DbSet<tbConfigs> Configs { get; set; }

        public DbSet<tbDestinations> Destinations { get; set; }

        public DbSet<tbGroupConfig> GroupConfig { get; set; }

        public DbSet<tbGroups> Groups { get; set; }

        public DbSet<tbLogs> Logs { get; set; }

        public DbSet<tbMacAddresses> MacAdresses { get; set; }

        public DbSet<tbSnapshots> Snapshots { get; set; }

        public DbSet<tbSources> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b2_zakharchenkoartem_db2;user=zakharchenkoarte;password=123456;SslMode=none");
        }

    }
}
