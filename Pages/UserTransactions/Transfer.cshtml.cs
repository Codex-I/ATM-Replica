using ATM_Replica.Data_folder.model_folder;
using ATM_Replica.Data_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace ATM_Replica.Pages.UserTransactions
{
    public class TransferModel : PageModel
    {
        private readonly ILogger<TransferModel> _logger;

        public TransferModel(ILogger<TransferModel> logger)
        {
            _logger = logger;
        }

        [Required]
        [BindProperty(SupportsGet =true)]
        public string? Email { get; set; }
        [BindProperty]
        public decimal InitialUserBalance { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal Balance { get; set; }
        [BindProperty]
        public string?  ReceiverEmail { get; set; }
         [BindProperty]
        public string? Description { get; set; }
        [BindProperty]
        [Range(1, 10000)]
        public int Amount { get; set; }
        [BindProperty]
        public decimal FormerReceiverbalance { get; set; }
        
        [BindProperty]
        public Transaction? TransferAmount { get; set; }
        [BindProperty]
        public bool TransferSuccess { get; set; } = false;

        public void OnGet()
        {
            var db = new Db();
            var getUserEmail = db.GetUser(Email);
            if (getUserEmail != null)
            {
                Balance = getUserEmail.Balance;
            }

        }
        public ActionResult OnPost()
        {
            try
            {

                var transAmount = Convert.ToDecimal(Amount);
                var db = new Db();

                var getReceiverEmail = db.GetUser(ReceiverEmail);
                if (getReceiverEmail != null)
                {

                    FormerReceiverbalance = getReceiverEmail.Balance + transAmount;
                    getReceiverEmail.Balance = FormerReceiverbalance;
                    db.UpdateUserInfo(getReceiverEmail);

                }
                else
                {

                    return Page();
                }

                var user = db.GetUser(Email);
                if (user != null)
                {
                    if (transAmount <= 0)
                    {
                        Balance = user.Balance;
                        ModelState.AddModelError("", "Your withdrawal amount must be positive");
                        throw new InvalidOperationException();
                    }
                    InitialUserBalance = user.Balance - transAmount;
                    user.Balance = InitialUserBalance;
                    db.UpdateUserInfo(user);

                    if (InitialUserBalance > 0)
                    {
                        Balance = user.Balance;
                    }
                }


                TransferAmount = new Transaction
                {
                    amount = transAmount,
                    TransactionType = "Transfer",
                    TransactionDateTime = DateTime.Now,
                    receiverEmail = ReceiverEmail,
                    description = Description,
                };

                if (InitialUserBalance < TransferAmount.amount)
                {
                    ModelState.AddModelError("", "Insufficient Balance");
                }
                TransferSuccess = true;
                db.CreateTransaction(TransferAmount);
                user.Transactions.Add(TransferAmount);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Page();
        }
    }
}
