using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace J104895.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new();

        public void OnGet()
        {
            // Retrieve the cart from the session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        public IActionResult OnPostUpdateQuantity(string itemName, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.Name == itemName);

            if (item != null)
            {
                item.Quantity = quantity; // Update quantity
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart); // Save updated cart
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveItem(string itemName)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.RemoveAll(i => i.Name == itemName); // Remove the item

            HttpContext.Session.SetObjectAsJson("Cart", cart); // Save updated cart
            return RedirectToPage();
        }
    }

    public class CartItem
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
