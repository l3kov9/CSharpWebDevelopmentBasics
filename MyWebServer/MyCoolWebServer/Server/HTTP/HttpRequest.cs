using System.Collections.Generic;
using MyCoolWebServer.Server.Enums;
using MyCoolWebServer.Server.HTTP.Contracts;

namespace MyCoolWebServer.Server.HTTP
{
    public class HttpRequest : IHttpRequest
    {
        public Dictionary<string, string> FormData => throw new System.NotImplementedException();

        public HttpHeaderCollection HeaderCollection => throw new System.NotImplementedException();

        public string Path => throw new System.NotImplementedException();

        public Dictionary<string, string> QueryParameters => throw new System.NotImplementedException();

        public HttpRequestMethod RequestMethod => throw new System.NotImplementedException();

        public string Url => throw new System.NotImplementedException();

        public Dictionary<string, string> UrlParameters => throw new System.NotImplementedException();

        public void AddUrlParameter(string key, string value)
        {
            throw new System.NotImplementedException();
        }
    }
}
