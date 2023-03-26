using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Tables;

namespace WebAPI
{
    public class BackupDatabase : DbContext
    {
        public DbSet<AdminsTb> Admins { get; set; }

        public DbSet<ComputersTb> Computers { get; set; }

        public DbSet<ConfigsTb> Configs { get; set; }

        public DbSet<GroupsTb> Groups { get; set; }

        public DbSet<ComputersConfigsTb> ComputersConfigs { get; set; }

        public DbSet<GroupsConfigsTb> GroupsConfigs { get; set; }

        public DbSet<ComputersGroupsTb> ComputersGroups { get; set; }

        public DbSet<MacAddressesTb> MacAddresses { get; set; }

        public DbSet<LogsTb> Logs { get; set; }

        public DbSet<DestinationsTb> Destinations { get; set; }

        public DbSet<SourcesTb> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=3b2_zakharchenkoartem_db2;user=zakharchenkoarte;password=123456;SslMode=none");
        }
    }
}
