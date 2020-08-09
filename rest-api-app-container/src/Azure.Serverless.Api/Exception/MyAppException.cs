namespace Azure.Web.Api.Exception
{
    public class MyAppException : System.Exception
    {
        public int ErrorCode { get; set; }
        public string Field { get; set; }
    }
}