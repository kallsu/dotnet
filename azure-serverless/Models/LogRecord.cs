namespace Azure.Serverless.Models {

    public class LogRecord {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Text { get; set; }
    }
}