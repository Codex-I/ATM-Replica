using ATM_Replica.Data_folder;
using ATM_Replica.Data_folder.model_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace ATM_Replica.Pages.UserTransactions
{
    public class ChangePaswwordModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string? Email { get; set; }
        [BindProperty]
        public string? OldPassword { get; set; }
        [BindProperty]
        public string? NewPassword { get; set; }
        [BindProperty]
        public string? Password { get; set; }
       

        public void OnGet()
        {
            
        }

        public ActionResult OnPost()
        {
            var db = new Db();
            var changeUserPassword = db.GetUser(Email);
            if (changeUserPassword != null)
            {
                if (changeUserPassword.Password == OldPassword)
                {
                    changeUserPassword.Password = NewPassword;
                    db.UpdateUserInfo(changeUserPassword);
                   ModelState.AddModelError("", "Password Successfully Changed");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Password");
                    return Page();
                }

            }
            return Page();
        }   
    }
}
