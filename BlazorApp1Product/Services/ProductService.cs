using BlazorApp1Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1Product.Services
{
	public class ProductCategoryService
	{
		private readonly HttpClient http;


		private string apiUrl = "https://localhost:7115/ProductCategories";

		public ProductCategoryService(HttpClient http)
		{
			this.http = http;
		}


		public Task<List<ProductCategory>> GetProductCategories()
		{
			return http.GetFromJsonAsync<List<ProductCategory>>(apiUrl);

			
		}
		public Task<ProductCategory> GetProductCategory(int id)
		{
			return http.GetFromJsonAsync<ProductCategory>(apiUrl+"/"+id);
		}
		public Task<HttpResponseMessage> SaveProductCategory(ProductCategory productCategory)
		{
			return http.PostAsJsonAsync<ProductCategory>(apiUrl, productCategory);
		}

		public Task<HttpResponseMessage> UpdateProductCategory(ProductCategory productCategory)
		{
			return http.PutAsJsonAsync<ProductCategory>(apiUrl + "/" + productCategory.ProductCategoryID, productCategory);
		}

		public Task<HttpResponseMessage> DeleteProductCategory(int id )
		{
			return http.DeleteAsync(apiUrl + "/" + id);
		}
	}
}
