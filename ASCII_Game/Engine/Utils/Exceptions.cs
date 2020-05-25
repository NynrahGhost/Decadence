using System;
using System.Collections.Generic;
using System.Text;

class ResourceException : System.Exception
{
    public ResourceException() : base() { }
    public ResourceException(string message) : base(message) { }
    public ResourceException(string message, Exception exception) : base(message, exception) { }
}

class INIException : System.Exception
{
    public INIException() : base() { }
    public INIException(string message) : base(message) { }
    public INIException(string message, Exception exception) : base(message, exception) { }
}

class INIFormatException : INIException
{
    public INIFormatException() : base() { }
    public INIFormatException(string message) : base(message) { }
    public INIFormatException(string message, Exception exception) : base(message, exception) { }
}