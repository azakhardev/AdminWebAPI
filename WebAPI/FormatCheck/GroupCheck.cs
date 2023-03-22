using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class GroupCheck
    {
        BackupDatabase dbBackup= new BackupDatabase();

        public void CheckAll(GroupsTb group)
        {
            GroupNameCheck(group);
            ComputerIDCheck(group);
        }

        public void GroupNameCheck(GroupsTb group)
        {
            if (Regex.IsMatch(group.GroupName, @"^[A-Z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid group name.");
        }

        public void ComputerIDCheck(GroupsTb group) 
        {
            //if (dbBackup.Computers.Find(group.ComputerID) != null)
            //    return;
            //throw new FormatException($"Computer with ID: {group.ComputerID} doesn't exist.");
        }

    }
}
