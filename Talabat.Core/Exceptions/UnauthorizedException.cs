using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Exceptions
{
    public sealed class UnauthorizedException(string message = "You are not authorized to access this resource.") :Exception(message)
    {
    }
}
