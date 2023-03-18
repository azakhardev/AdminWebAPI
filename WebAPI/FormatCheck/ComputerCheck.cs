using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class ComputerCheck
    {
        BackupDatabase dbBackup = new BackupDatabase();
        public void CheckAll(tbComputers computer)
        {
            CheckComputerName(computer);
            LastBackupChechk(computer);
        }

        public void CheckComputerName(tbComputers computer)
        {
            if (Regex.IsMatch(computer.ComputerName, @"^[A-Z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid computer name");
        }

        public void LastBackupChechk(tbComputers computer)
        {
            if (computer.LastBackup > DateTime.Now)
                return;
            throw new FormatException("Last backup cant be in the future");
        }

        //public void CheckSources(tbSources source)
        //{
        //    if (Regex.IsMatch(source.SourcePath, @"^[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$"))
        //        return;
        //    throw new FormatException("Invalid source");
        //}

        //public void CheckDestiantions(tbDestinations destination)
        //{            
        //    if (Regex.IsMatch(destination.DestinationPath, @"^[A-Za-z]:\\(?:[^\\/:*?""<>|\r\n]+\\)*[^\\/:*?""<>|\r\n]*$"))
        //        return;
        //    throw new FormatException("Invalid destination");
            
        //}
    }
}
