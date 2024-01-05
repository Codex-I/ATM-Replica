using ATM_Replica.Data_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM_Replica.Pages.UserTransactions
{
    public class CheckBalanceModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string? Email { get; set; }
        [BindProperty(SupportsGet =true)]
        public decimal Balance { get; set; }

        public void OnGet()
        {
            var getUser = new Db();
            var users = getUser.GetUser(Email);
           
                if(users.Email == Email)
                {
                    Balance = users.Balance;
                }

                
            
        }


    }
}
