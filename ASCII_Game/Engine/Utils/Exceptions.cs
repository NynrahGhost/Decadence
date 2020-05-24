using System;
using System.Collections.Generic;
using System.Text;

class ResourceException : System.Exception
{
    public ResourceException() : base() { }
    public ResourceException(string message) : base(message) { }
    public ResourceException(string message, Exception exception) : base(message, exception) { }
}