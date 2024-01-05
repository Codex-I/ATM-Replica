using ATM_Replica.Data_folder;
using ATM_Replica.Data_folder.model_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ATM_Replica.Pages
{
    public class WithdrawPageModel : PageModel
    {
        [Required]
        [BindProperty]
        [Range(1, 1000000)]
        public int Amount { get; set; }
        [BindProperty]
        public Transaction? WithdrawAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal Balance { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? LastName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? FirstName { get; set; }
        

        public void OnGet()
        {
            var db = new Db();
            var user = db.GetUser(Email);
            if (user != null)
            {
                Balance = user.Balance;
            }
        }
        public void OnPost()
        {
            try
            {
                decimal howMuch = Convert.ToDecimal(Amount);

                var db = new Db();
                var user = db.GetUser(Email);

                if (user != null)
                {
                    if (howMuch <= 0)
                    {
                        Balance = user.Balance;
                        ModelState.AddModelError("", "Your withdrawal amount must be positive");
                       throw new InvalidOperationException();
                    }
                    
                    if (howMuch < user.Balance)
                    {
                        var newBalance = user.Balance - howMuch;
                        user.Balance = newBalance;

                    }
                    else if (howMuch == user.Balance)
                    {
                        ModelState.AddModelError("", "You cannot Empty your account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Insufficient Balance");
                    }
                    if (user.Balance > 0)
                    {
                        Balance = user.Balance;
                    }
                    FirstName = user.FirstName;
                    LastName = user.LastName;

                }

                WithdrawAmount = new Transaction
                {
                    TransactionType = "Withdraw",
                    amount = howMuch,
                    TransactionDateTime = DateTime.Now,
                };
                db.CreateTransaction(WithdrawAmount);
                db.GetListOfTransForAUser(user);
                db.UpdateUserInfo(user);
                user.Transactions.Add(WithdrawAmount);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}








