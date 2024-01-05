using ATM_Replica.Data_folder;
using ATM_Replica.Data_folder.model_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ATM_Replica.Pages.UserTransactions
{
    public class PayBillsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }
        [BindProperty]
        public decimal InitialBalance { get; set; }

        [BindProperty]
        public decimal Balance { get; set; }
        [BindProperty(SupportsGet =true)]
        public string? SelectedBiller { get; set; }
        [BindProperty(SupportsGet =true)]
        public string? RecieverAccountEmail { get; set; }
        [BindProperty]
        public decimal FormerReceiverbalance { get; set; }
        [BindProperty]
        public decimal UpdatedReceiverBalance { get; set; }
        [BindProperty]
        public decimal Amount { get; set; }
        [BindProperty]
        public Transaction? BillTransaction { get; set; }



        public void OnGet()
        {
            var UserPaybill = new Db();
            var GetUser = UserPaybill.GetUser(Email);
            
                if (GetUser.Email == Email)
                {
                    Balance = GetUser.Balance;
                }
            
            
        }
        public void OnPost()
        {
            try
            {
                decimal billAmount = Convert.ToDecimal(Amount);

                var db = new Db();
                var user = db.GetUser(Email) ;
                if (user != null && user.Email == Email)
                {
                    if (billAmount <= 0)
                    {
                        Balance = user.Balance;
                        ModelState.AddModelError("", "Your withdrawal amount must be positive");
                        throw new InvalidOperationException();
                    }
                    InitialBalance = user.Balance;
                    var updateBalance = user.Balance - billAmount;
                    if (updateBalance >= 0)
                    {
                        Balance = updateBalance;
                        user.Balance = updateBalance;
                        db.UpdateUserInfo(user);
                    }
                   
                }

                var receiverEmail = db.GetUser(RecieverAccountEmail);
                if (receiverEmail.Email == RecieverAccountEmail)
                {
                    FormerReceiverbalance = receiverEmail.Balance;
                    UpdatedReceiverBalance = receiverEmail.Balance + billAmount;
                    receiverEmail.Balance = UpdatedReceiverBalance;
                    db.UpdateUserInfo(receiverEmail);
                }


                BillTransaction = new Transaction
                {
                    amount = billAmount,
                    TransactionType = "Subscription",
                    TransactionDateTime = DateTime.Now,
                    receiverEmail = RecieverAccountEmail,
                };

                if (InitialBalance < billAmount)
                {
                    ModelState.AddModelError("", "Insufficient Balance");
                }
                db.CreateTransaction(BillTransaction);
                user.Transactions.Add(BillTransaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
