using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace J104895.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "The First Name is required")]
        public string FirstName { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "The Last Name is required")]
        public string LastName { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; } = "";
        [BindProperty]
        public string? Phone { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "The Subject is required")]
        public string Subject { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "The Message is required")]
        public string Message { get; set; } = "";

        public string successMessage = "";
        public string errorMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Data Validation is failed";
                return;
            }

            successMessage = "Your message has been received successfully";

            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Subject = "";
            Message = "";

            ModelState.Clear();
        }
    }
}
