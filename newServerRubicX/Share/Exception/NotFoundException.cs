using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
