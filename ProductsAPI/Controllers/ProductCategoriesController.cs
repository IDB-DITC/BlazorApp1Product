using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductsAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProductCategoriesController : ControllerBase
	{
		private readonly ProductsAPIContext _context;
		private readonly IWebHostEnvironment webHost;

		public ProductCategoriesController(ProductsAPIContext context, IWebHostEnvironment webHost)
		{
			_context = context;
			this.webHost = webHost;
		}

		// GET: ProductCategories
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
		{
			return await _context.ProductCategory.Include(pc => pc.Products).ToListAsync();
		}

		// GET: ProductCategories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
		{
			var productCategory = await _context.ProductCategory.Include(pc => pc.Products).SingleAsync(pc => pc.ProductCategoryID == id);

			if (productCategory == null)
			{
				return NotFound();
			}

			return productCategory;
		}

		// PUT: ProductCategories/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
		{
			if (id != productCategory.ProductCategoryID)
			{
				return BadRequest();
			}

			if (productCategory.ImageData != null)
			{
				//save base64 image to file system


				byte[] imageBytes = Convert.FromBase64String(productCategory.ImageData.Base64Data);



				string fileName = "/images/" + DateTime.Now.ToString("yyMMddhhmmss-") + productCategory.ImageData.FileName;


				string filePath = this.webHost.WebRootPath + fileName;

				await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);


				productCategory.ImagePath = (HttpContext.Request.IsHttps ? "https://" : "http://") + HttpContext.Request.Host + fileName;



			}
			try
			{
				_context.Update(productCategory);

				var itemsIdList = productCategory.Products?.Select(i => i.ProductID).ToList();

				var delItems = await _context.Product.Where(i => i.ProductCategoryID == id).Where(i => !itemsIdList.Contains(i.ProductID)).ToListAsync();

				_context.Product.RemoveRange(delItems);


				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductCategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: ProductCategories
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
		{








			if (productCategory.ImageData != null)
			{
				//save base64 image to file system


				byte[] imageBytes = Convert.FromBase64String(productCategory.ImageData.Base64Data);



				string fileName = "/images/" + DateTime.Now.ToString("yyMMddhhmmss-") + productCategory.ImageData.FileName;


				string filePath = this.webHost.WebRootPath + fileName;

				await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);


				productCategory.ImagePath = (HttpContext.Request.IsHttps ? "https://" : "http://") + HttpContext.Request.Host + fileName;



			}
			_context.ProductCategory.Add(productCategory);

			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryID }, productCategory);
		}

		// DELETE: ProductCategories/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductCategory(int id)
		{
			var productCategory = await _context.ProductCategory.FindAsync(id);
			if (productCategory == null)
			{
				return NotFound();
			}

			_context.ProductCategory.Remove(productCategory);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductCategoryExists(int id)
		{
			return _context.ProductCategory.Any(e => e.ProductCategoryID == id);
		}
	}
}
