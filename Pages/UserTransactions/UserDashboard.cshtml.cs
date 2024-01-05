using ATM_Replica.Data_folder;
using ATM_Replica.Data_folder.model_folder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM_Replica.Pages
{
    public class UserDashboardModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string? Email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? FirstName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? LastName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Password { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal Balance { get; set; }


        public ActionResult OnGet()
        {
            try
            {
                //var userfullInfo = new Db();
                var db = new Db();
                var userInfo = db.GetUser(Email);

                if (userInfo.Email == Email)
                {
                    FirstName = userInfo.FirstName;
                    LastName = userInfo.LastName;
                }

             

            }
            catch(Exception ex)
            {
                Console.WriteLine("Message", ex);
            }


            return Page();
        }

       

    }
}
