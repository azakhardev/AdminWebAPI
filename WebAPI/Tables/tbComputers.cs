using MySql.EntityFrameworkCore.Extensions;
using Org.BouncyCastle.Tls.Crypto;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Tables
{
    [Table("Computers")]
    public class tbComputers
    {
        public int ID { get; set; }
        public string ComputerName { get; set; }
        public string ComputerStatus { get; set; }
        public string Description { get; set; }
        public DateTime LastBackup { get; set; }
        public string BackupStatus { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbMacAddresses> MacAddresses { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbComputersConfigs> ComputersConfigs { get; set; }

        [ForeignKey("ComputerID")]
        public virtual List<tbGroups> Groups { get; set; }

        public List<string> GetMacAddresses(tbComputers computer, BackupDatabase dbBackup) 
        {
            List<tbMacAddresses> tbMacAddresses = dbBackup.MacAdresses.Where(x => x.ComputerID == computer.ID).ToList();
            List<string> macAddresses = new List<string>();

            foreach (var item in tbMacAddresses)
            {
                macAddresses.Add(item.MacAddress);
            }

            return macAddresses;
        }

        public List<int> GetConfigs(tbComputers computer, BackupDatabase dbBakcup)
        {
            List <tbMacAddresses> macAddresses = dbBakcup.ComputersConfigs.Where(x => x.ID == computer.ComputersConfigs).Single();
            foreach (var config in MacAddresses) { }
        }
    }
}
