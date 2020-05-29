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



class JSONException : System.Exception
{
    public JSONException() : base() { }
    public JSONException(string message) : base(message) { }
    public JSONException(string message, Exception exception) : base(message, exception) { }
}

class JSONFormatException : JSONException
{
    public JSONFormatException() : base() { }
    public JSONFormatException(string message) : base(message) { }
    public JSONFormatException(string message, Exception exception) : base(message, exception) { }
}