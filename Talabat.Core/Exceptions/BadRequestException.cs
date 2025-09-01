using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Exceptions
{
    public sealed class BadRequestException(string message = "The request is invalid.") : Exception(message)
    {
    }
}
