namespace WebAPI.Tables.Help_Tables
{
    public class LogsForConfigs
    {
        public int ID { get; set; }

        public int ConfigID { get; set; }

        public DateTime Date { get; set; }

        public bool Errors { get; set; }

        public string Message { get; set; }
    }
}
