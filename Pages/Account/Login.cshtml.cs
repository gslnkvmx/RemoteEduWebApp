using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Models;

namespace RemoteEduApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public new User User { get; set; } = new User();
        public void OnGet()
        {
        }
    }
}
