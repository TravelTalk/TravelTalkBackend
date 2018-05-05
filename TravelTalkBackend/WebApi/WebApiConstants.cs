namespace TravelTalk.WebApi {

    internal static class WebApiConstants {
        
        public const string CONTROLLER_SUFFIX = "Controller";

        public const string API_ROUTES_PREFIX = "api";

        public const string FAVICON_ROUTE = "{*favicon}";
        public const string MATCH_ALL_ROUTE_PART = "{*url}";
        public const string CONTROLLER_ROUTE_PART = "{controller}";
        public const string ACTION_ROUTE_PART = "{action}";

        public const string HTTP_VERB_OPTIONS = "OPTIONS";
        public const string HTTP_VERB_GET = "GET";
        public const string HTTP_VERB_POST = "POST";
        public const string HTTP_VERB_PUT = "PUT";
        public const string HTTP_VERB_DELETE = "DELETE";
    }
}
