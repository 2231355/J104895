using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace J104895.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public string? Name { get; set; }
        [BindProperty]
        public string? Email { get; set; }
        [BindProperty]
        public string? Address { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public void OnGet()
        {
            // Retrieve cart items from session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        public IActionResult OnPost()
        {
            // Retrieve cart items from session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            if (CartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty. Please add items to your cart.");
                return Page();
            }

            // Simulate order processing (store user and order details, clear the cart, etc.)
            HttpContext.Session.Remove("Cart"); // Clear the cart

            // Redirect to a confirmation page
            TempData["Name"] = Name;
            TempData["OrderTotal"] = CartItems.Sum(i => i.Price * i.Quantity);
            return RedirectToPage("/OrderConfirmation");
        }
    }
}
