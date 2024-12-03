using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using J104895.Models; // Import the namespace for the models

namespace J104895.Pages
{
    public class MenuModel : PageModel
    {
        public List<MenuItem> MenuItems { get; set; } = new();
        private List<MenuItem> AllMenuItems { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        [BindProperty]
        public string? ItemName { get; set; }

        [BindProperty]
        public double ItemPrice { get; set; }

        public void OnGet()
        {
            // Initialize the menu items
            AllMenuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Buttermilk With Butter Egg", Description = "Buttermilk With Butter Egg chicken with rice.", Price = 5.80, Image = "food1.jpg" },
                new MenuItem { Name = "Pop-up Beef Burger", Description = "Burger with Homemade Beef patty, cheese, mayonaise sauce, and french fries.", Price = 10.00, Image = "food2.jpg" },
                new MenuItem { Name = "Mee Goreng", Description = "Traditional dry noodles with a mince chicken or beef.", Price = 2.50, Image = "food3.jpg" },
                new MenuItem { Name = "Peanut Butter & Kaya Waffle", Description = "Crispy waffle fill in with Peanut Butter & Kaya jam.", Price = 2.00, Image = "food4.jpg" },
                new MenuItem { Name = "Chocolate cake", Description = "Slice of chocolate cake.", Price = 4.00, Image = "food5.jpg" }
            };

            // Filter items based on search query if provided
            MenuItems = string.IsNullOrEmpty(SearchQuery)
                ? AllMenuItems
                : AllMenuItems
                    .Where(item => item.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        public IActionResult OnPost()
        {
            // Ensure the session is available
            if (HttpContext.Session == null)
            {
                return RedirectToPage();
            }

            // Retrieve existing cart from session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Add the selected item to the cart
            if (!string.IsNullOrEmpty(ItemName))
            {
                cart.Add(new CartItem
                {
                    Name = ItemName,
                    Price = ItemPrice,
                    Quantity = 1
                });
            }

            // Save updated cart to session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToPage();
        }
    }
}
