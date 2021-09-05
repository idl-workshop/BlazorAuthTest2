using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IDL.Core;
using IDL.Core.Data;
using IDL.Core.Api;

namespace BlazorAuthTest2.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        // https://app.pluralsight.com/course-player?clipId=a8e78c0f-b40b-40f8-b13c-26528b6c3d7e
        public async Task<IActionResult> OnPostAsync()
        {

            // Authenticate
            IDL.Core.Data.User user;

            // BUG: handle async exception
            try
            {
                user = await AuthService.LoginUserAsync(Username, Password, "IDLM");

                var claims = new List<Claim>();

                // User info
                if (!string.IsNullOrWhiteSpace(user.username)) // username (unique)
                    claims.Add (new Claim(ClaimTypes.Name, user.username));
                if (!string.IsNullOrWhiteSpace(user.email))
                    claims.Add (new Claim(ClaimTypes.Email, user.email));
                if (!string.IsNullOrWhiteSpace(user.name))
                    claims.Add (new Claim(ClaimTypes.GivenName, user.name));

                // Roles

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                    );

                return LocalRedirect(Url.Content("~/"));
            }
            catch (Exception ex)
            {
                // Fail handler
                return Page();
            }

            if (user == null)
            {
                // Fail handler
                return Page();
            }

        }

    }
}
