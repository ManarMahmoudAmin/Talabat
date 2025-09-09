using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Dtos.BasketDtos;
using Talabat.Core.Entities.BasketModule;
using Talabat.Core.Exceptions;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service.Services
{
	public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
	{
		public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto Basket)
		{
			var basket = _mapper.Map<CustomerBasket>(Basket);
			var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
			if (CreatedOrUpdatedBasket is not null)
				return _mapper.Map<BasketDto>(CreatedOrUpdatedBasket);
			else
				throw new Exception("Cannot Create Or Update Basket Now. Try Again Later");
		}

		public async Task<bool> DeleteBasketAsync(string Key)
			=> await _basketRepository.DeleteBasketAsync(Key);

		public async Task<BasketDto> GetBasketAsync(string Key)
		{
			var basket = await _basketRepository.GetBasketAsync(Key);
			if (basket is not null)
				return _mapper.Map<BasketDto>(basket);
			else
				throw new BasketNotFoundException(Key);
		}
	}
}
