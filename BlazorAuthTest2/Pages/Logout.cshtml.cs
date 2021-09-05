using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorAuthTest2.Pages
{
    public class LogoutModel : PageModel
    {

        // https://app.pluralsight.com/course-player?clipId=a95e47f6-2661-4e6d-8c36-0c65c07040f3
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect(Url.Content("~/"));
        }


    }
}
