using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class ComputerCheck
    {
        BackupDatabase dbBackup = new BackupDatabase();

        public void CheckAll(ComputersTb computer)
        {
            CheckComputerName(computer);
            LastBackupCheck(computer);
        }

        public void CheckComputerName(ComputersTb computer)
        {
            if (Regex.IsMatch(computer.ComputerName, @"^[A-Z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid computer name");
        }

        public void LastBackupCheck(ComputersTb computer)
        {
            if (computer.LastBackup > DateTime.Now)
                return;
            throw new FormatException("Last backup cant be in the future");
        }

        public void CheckConfigID(ComputersTb computer) 
        {
            if (dbBackup.Configs.Find(computer.ID) != null) 
            {

            }
        }
    }
}
