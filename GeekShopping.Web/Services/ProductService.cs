﻿using System.Net.Http.Headers;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IService;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _client;
		public const string BasePath = "api/v1/product";

		public ProductService(HttpClient client)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public async Task<IEnumerable<ProductModel>> FindAllProducts(string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetAsync(BasePath);
			return await response.ReadContentAs<List<ProductModel>>();
		}

		public async Task<ProductModel> FindProductById(long id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.GetAsync($"{BasePath}/{id}");
			return await response.ReadContentAs<ProductModel>();
		}

		public async Task<ProductModel> CreateProduct(ProductModel model, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PostAsJson(BasePath, model);
			if (response.IsSuccessStatusCode)
				return await response.ReadContentAs<ProductModel>();
			else throw new Exception("Something went wrong when calling API");
		}
		public async Task<ProductModel> UpdateProduct(ProductModel model, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.PutAsJson(BasePath, model);
			if (response.IsSuccessStatusCode)
				return await response.ReadContentAs<ProductModel>();
			else throw new Exception("Something went wrong when calling API");
		}

		public async Task<bool> DeleteProduct([FromBody] long id, string token)
		{
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			var response = await _client.DeleteAsync($"{BasePath}/{id}");
			if (response.IsSuccessStatusCode)
			{
				//return await response.ReadContentAs<bool>();
				return true;
			}
			else
			{
				//throw new Exception($"Algo deu errado, {response.ReasonPhrase}");
				return false;
			}
		}
	}
}
