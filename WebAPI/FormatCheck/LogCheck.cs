namespace WebAPI.FormatCheck
{
    public class LogCheck
    {
        public void CheckAll()
        {

        }

        public void ComputersConfigsIDCheck(int computerConfigID)
        {
            if (true)
                return;
            throw new FormatException("Invalid computer config ID");
        }

        public void DateCheck(DateTime Date)
        {
            if (true)
                return;
            throw new FormatException("Invalid date");
        }
    }
}
