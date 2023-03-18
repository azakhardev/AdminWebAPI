using WebAPI.Tables;

namespace WebAPI.FormatCheck
{
    public class GroupCheck
    {
        public void CheckAll(tbGroups group)
        {
            GroupNameCheck(group);
            ComputerIDCheck(group);
        }

        public void GroupNameCheck(tbGroups group)
        {
            if (true)
                return;
            throw new FormatException("Invalid group name");
        }

        public void ComputerIDCheck(tbGroups group)
        {
            if (true)
                return;
            throw new FormatException("Invalid computer ID");
        }
    }
}
