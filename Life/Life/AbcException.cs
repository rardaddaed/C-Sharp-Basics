using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    public class AbcException : Exception
    {
        public AbcException(string message) : base("The number of rows and columns must be within 4 - 48 (inclusive)!")
        {

        }
    }
}
