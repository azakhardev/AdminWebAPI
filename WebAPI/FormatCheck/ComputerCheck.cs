using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class ComputerCheck
    {
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
    }
}
