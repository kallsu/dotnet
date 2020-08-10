namespace Azure.Web.Api.Commons
{
    public static class MyCustomErrorCodes
    {
        public const int SESSION_EXPIRED = 403;

        public const int COUNTRY_ID_UNPARSABLE = 401;
        public const int COUNTRY_NOT_FOUND = 402;

        public static int DISTRICT_NOT_FOUND = 403;
        public static int DISTRICT_ID_UNPARSABLE = 404;
    }
}