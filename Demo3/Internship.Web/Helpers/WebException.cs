using System;
using System.Globalization;

namespace Internship.Web
{
    public class WebException : Exception
    {
        public WebException() : base() { }

        public WebException(string message) : base(message) { }

        public WebException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}