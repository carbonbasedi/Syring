using Business.Services.User.Abstract;
using Business.ViewModels.User.Cart;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.User.Concrete
{
	public class CartService : ICartService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IBasketProductRepository _basketProductRepository;
		private readonly IProductRepository _productRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CartService(IBasketRepository basketRepository,
                            IBasketProductRepository basketProductRepository,
							IProductRepository productRepository,
							IUnitOfWork unitOfWork)
        {
			_basketRepository = basketRepository;
			_basketProductRepository = basketProductRepository;
			_productRepository = productRepository;
			_unitOfWork = unitOfWork;
		}
		public async Task<List<CartProductVM>> Index(IdentityUser user)
		{
			var cart = await _basketRepository.GetUserBasketWithProducts(user.Id);

			var model = new List<CartProductVM>();
			if(cart == null) return model;

			foreach(var cartProduct in cart.BasketProducts)
			{
				var cartProductItem = new CartProductVM
				{
					Id = cartProduct.Id,
					Count = cartProduct.Count,
					Image = cartProduct.Product.Image,
					Price = cartProduct.Product.Price,
					Stock = cartProduct.Product.Stock,
					Title = cartProduct.Product.Name
				};
				model.Add(cartProductItem);
			}
			return model;
		}
		public async Task<bool> AddAsync(IdentityUser user, int id)
		{
			var cart = await _basketRepository.GetBasket(user.Id);
			if(cart is null)
			{
				cart = new Basket
				{
					UserId = user.Id,
				};
				await _basketRepository.Create(cart);
			}
			var product = await _productRepository.GetAsync(id);
			if(product is null) return false;

			var cartProduct = await _basketProductRepository.GetUserBasket(user.Id, id);

			if(cartProduct is null)
			{
				cartProduct = new BasketProduct
				{
					Basket = cart,
					ProductId = product.Id,
					Count = 1
				};
				await _basketProductRepository.CreateAsync(cartProduct);
			}
			else
			{
				cartProduct.Count++;
				_basketProductRepository.Update(cartProduct);
			}

			await _unitOfWork.CommitAsync();
			return true;
		}
		public async Task<bool> IncreaseAsync(IdentityUser user, int id)
		{
			var cart = await _basketRepository.GetBasket(user.Id);
			if(cart is null) return false;

			var cartProduct = await _basketProductRepository.GetBasketProduct(cart.Id, id);
			if(cartProduct is null) return false;

			var product = await _productRepository.GetAsync(cartProduct.ProductId);
			if(product is null) return false;

			if (product.Stock == cartProduct.Count) return false;

			cartProduct.Count++;
			_basketProductRepository.Update(cartProduct);
			await _unitOfWork.CommitAsync();
			return true;
		}
		public async Task<bool> DecreaseAsync(IdentityUser user, int id)
		{
			var cart = await _basketRepository.GetBasket(user.Id);
			if (cart is null) return false;

			var cartProduct = await _basketProductRepository.GetBasketProduct(cart.Id, id);
			if(cartProduct is null) return false;

			var product = await _productRepository.GetAsync(cartProduct.ProductId);
			if(product is null) return false;

			if(cartProduct.Count == 0)
				cart.BasketProducts.Remove(cartProduct);

			cartProduct.Count--;
			_basketProductRepository.Update(cartProduct);
			await _unitOfWork.CommitAsync();

			return true;
		}
		public async Task<bool> DeleteAsync(IdentityUser user, int id)
		{
			var cart = await _basketRepository.GetBasket(user.Id);
			if (cart is null) return false;

			var cartProduct = await _basketProductRepository.GetBasketProduct(cart.Id, id);
			if (cartProduct is null) return false;

			var product = await _productRepository.GetAsync(cartProduct.ProductId);
			if (product is null) return false;

			_basketProductRepository.Delete(cartProduct);
			await _unitOfWork.CommitAsync();

			return true;
		}
	}
}
