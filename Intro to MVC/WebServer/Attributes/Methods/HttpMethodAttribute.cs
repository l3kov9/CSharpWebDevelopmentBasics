using System;

namespace WebServer.Attributes.Methods
{
    public abstract class HttpMethodAttribute : Attribute
    {
        public abstract bool IsValid(string requestMethod);
    }
}
