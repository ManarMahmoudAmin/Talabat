using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.BasketDtos;

namespace Talabat.Core.Services.Contract
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
