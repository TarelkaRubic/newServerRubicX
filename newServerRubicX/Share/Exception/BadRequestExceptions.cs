using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Exception
{
    public class BadRequestExceptions: System.Exception
    {
        public BadRequestExceptions(string message) : base(message)
        {
            
        }
    }
}
