namespace WebAPI.FormatCheck
{
    public class GroupCheck
    {
        public void CheckAll()
        {
            
        }

        public void GroupNameCheck(string groupName)
        {
            if (true)
                return;
            throw new FormatException("Invalid group name");
        }

        public void ComputerIDCheck(string computerID)
        {
            if (true)
                return;
            throw new FormatException("Invalid computer ID");
        }
    }
}
