using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ExceptionFecha : Exception
    {
        public ExceptionFecha(string Mensaje) : base(Mensaje) 
        { }
    }
}
