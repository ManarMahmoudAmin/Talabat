using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Exceptions
{
    public sealed class BasketNotFoundException(string id): NotFoundException($"Basket with id = {id} is Not Found")
    {
    }
}
