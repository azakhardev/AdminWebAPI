using System.Text.RegularExpressions;
using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class GroupCheck
    {
        public void CheckAll(tbGroups group)
        {
            GroupNameCheck(group);
        }

        public void GroupNameCheck(tbGroups group)
        {
            if (Regex.IsMatch(group.GroupName, @"^[A-Z0-9_]{3,50}$"))
                return;
            throw new FormatException("Invalid group name");
        }

    }
}
