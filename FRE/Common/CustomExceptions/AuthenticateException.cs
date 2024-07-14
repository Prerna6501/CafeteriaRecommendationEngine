using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomExceptions
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException(string message) : base(message) 
        {
                
        }
    }
}
