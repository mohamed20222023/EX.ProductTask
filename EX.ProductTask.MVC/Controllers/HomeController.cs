using Application.Common.Pagination;
using Application.Dtos.Product;
using Application.IBusiness.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{

	[Authorize]
	public class HomeController : Controller
	{
		private readonly IProductBusiness _productBusiness;
		private readonly IConfiguration _configuration;


		public HomeController(IProductBusiness productBusiness, IConfiguration configuration)
		{
			_productBusiness = productBusiness;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productBusiness.GetAllProducts();
			return View(products);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AdminIndex()
		{
			var products = await _productBusiness.GetAllProducts();
			return View(products);
		}


		public async Task<IActionResult> Detail(int id)
		{
			var product = await _productBusiness.GetByIdAsync(id);

			if (product == null)
			{
				return RedirectToAction(nameof(NotFound));
			}
			return View(product);
		}


		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create()
		{

			var categories = await _productBusiness.GetAllCategories();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([FromForm] ProductEditDto product)
		{

			var categories = _productBusiness.GetAllCategories();
			if (product.File is not null)
			{
				if (!IsValidFileType(product.File.FileName))
				{
					ModelState.AddModelError(string.Empty, "Invalid Image Extension");
					ViewBag.Categories = new SelectList(await categories, "Id", "Name");
					return View(product);
				}
				if (!IsValidFileType(product.File.FileName))
				{
					ModelState.AddModelError(string.Empty, "Image Size Greater than 1 M");
					ViewBag.Categories = new SelectList(await categories, "Id", "Name");
					return View(product);
				}
			}
			ViewBag.Categories = new SelectList(await categories, "Id", "Name");
			var result = _productBusiness.GetAllCategories(); ;
			await _productBusiness.Register(product);
			return RedirectToAction(nameof(AdminIndex));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
			var categories = _productBusiness.GetAllCategories();
			ViewBag.Categories = new SelectList(await categories, "Id", "Name");
			var product = await _productBusiness.GetByIdAsync(id);
			if (product == null)
			{
				return RedirectToAction(nameof(NotFound));
			}
			return View(product);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [FromForm] ProductEditDto product)
		{
			var categories = _productBusiness.GetAllCategories();

			if (product.File is not null)
			{
				if (!IsValidFileType(product.File.FileName))
				{
					ModelState.AddModelError(string.Empty, "Invalid Image Extension");
					ViewBag.Categories = new SelectList(await categories, "Id", "Name");
					return View(product);
				}
				if (!IsValidFileType(product.File.FileName))
				{
					ModelState.AddModelError(string.Empty, "Image Size Greater than 1 M");
					ViewBag.Categories = new SelectList(await categories, "Id", "Name");
					return View(product);
				}
			}

			ViewBag.Categories = new SelectList(await categories, "Id", "Name");
			await _productBusiness.Edit(id, product);
			return RedirectToAction(nameof(AdminIndex));
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _productBusiness.GetByIdAsync(id);
			if (employee == null)
			{
				return RedirectToAction(nameof(NotFound));
			}
			return View(employee);
		}


		[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _productBusiness.DeleteById(id);
			return RedirectToAction(nameof(AdminIndex));
		}


		private bool IsValidFileType(string fileName)
		{
			var types = _configuration.GetSection("attachment:requestAttachment").Value;
			var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
			return types.Contains(fileExtension);
		}
		private bool IsValidFileSize(long fileSize)
		{
			var types = _configuration.GetSection("attachment:attachmentSize").Value;
			return fileSize <= int.Parse(types) * 1024 * 1024;
		}
	}
}
