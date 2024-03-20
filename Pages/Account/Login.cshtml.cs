using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemoteEduApp.Data;
using RemoteEduApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace RemoteEduApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public new User User { get; set; } = new User();

        DataContextDapper _dapper;

        string _errorMessage = "";

        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }

        public LoginModel(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) return Page();

            string sql = "SELECT * FROM [RemoteEduDB].[dbo].[User] WHERE [Login] = '" + User.Login + "' AND  [Password] = '" + User.Password + "';";

            User? getUser = null;

            try {
                getUser = _dapper.LoadDataSingle<User>(sql);
            }

            catch (InvalidOperationException ex) {
                ErrorMessage = "¬веден неверный логин или пароль!";
            }

            if (getUser != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim("Id", getUser.Id.ToString()),
                    new Claim("Login", getUser.Login.Replace(" ", "")),
                    new Claim("Role", getUser.Role.Replace(" ", ""))
                };
                var identity = new ClaimsIdentity(claims, "AuthCookie");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AuthCookie", claimsPrincipal);

                //return RedirectToPage("/" +getUser.Role.Replace(" ", "") + "/MainPage");
                return RedirectToPage("/Student/MainPage");
            }

            return Page();
        }
    }
}
