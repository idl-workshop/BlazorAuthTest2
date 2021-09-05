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
            
            if(!(Username == "test" && Password == "test"))
            {
                // Fail handler
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Michael Wells"),
                new Claim(ClaimTypes.Email, "mike@sygnal.com")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
                );

            return LocalRedirect(Url.Content("~/"));
        }

    }
}
