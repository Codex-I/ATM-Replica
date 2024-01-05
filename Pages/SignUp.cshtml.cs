using ATM_Replica.Data_folder.model_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using ATM_Replica.Data_folder;

namespace ATM_Replica.Pages
{
    public class SignUpModel : PageModel
    {

        [BindProperty]
        public string? FirstName { get; set; }

        [BindProperty]
        public string? LastName { get; set; }

        [BindProperty]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [BindProperty]
        public User.AccountTypeEnum AccountType { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? ErrorMessage { get; private set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("", "The password and confirmation password do not match.");
                return Page();
            }

            var db = new Db();
            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                AccountType = AccountType,
                Password = Password,
                Balance = 0.00M
            };
            // create the new user document
            db.CreateUser(newUser);

            // Redirect to new Dashboard
            return RedirectToPage("/UserTransactions/UserDashboard", new { Email });
        }

    }

}








