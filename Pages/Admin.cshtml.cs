using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;

namespace J104895.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public IFormFile? UploadedImage { get; set; }

        public List<Product> Products { get; set; } = new();

        private readonly IWebHostEnvironment _environment;

        public AdminModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
            // Load existing products (replace with a database query if needed)
            Products = ProductRepository.GetAll();
        }

        public IActionResult OnPost()
        {
            if (UploadedImage != null)
            {
                // Save the image to the wwwroot/images folder
                var imagePath = Path.Combine(_environment.WebRootPath, "images", UploadedImage.FileName);
                using var stream = new FileStream(imagePath, FileMode.Create);
                UploadedImage.CopyTo(stream);

                Product.Image = UploadedImage.FileName;
            }

            if (Product.Id == 0)
            {
                // Add a new product
                ProductRepository.Add(Product);
            }
            else
            {
                // Update the existing product
                ProductRepository.Update(Product);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEdit(int id)
        {
            Product = ProductRepository.GetById(id) ?? new Product();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            ProductRepository.Delete(id);
            return RedirectToPage();
        }
    }

    // Product Model
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
    }

    // Simple in-memory repository for demonstration purposes
    public static class ProductRepository
    {
        private static List<Product> _products = new();
        private static int _nextId = 1;

        public static List<Product> GetAll() => _products;

        public static Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public static void Add(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
        }

        public static void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Image = product.Image;
            }
        }

        public static void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
