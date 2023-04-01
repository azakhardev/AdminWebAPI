namespace WebAPI.Tables.Help_Tables
{
    public class LogsForPC
    {
        public int ID { get; set; }

        public int ComputerID { get; set; }

        public DateTime Date { get; set; }

        public bool Errors { get; set; }

        public string Message { get; set; }
    }
}
