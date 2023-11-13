using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace LayoutTemplateWebApp.Pages
{
    public class LoginModel : PageModel
    {
        //private readonly ILogger<LoginModel> _logger;
        public string email { get; set; }
        public string password { get; set; }

        public bool showErrorMsg = false;

        public bool showIncorrect = false;
        public LoginModel()
        {
        }

        public void OnGet()
        {
            //Debug.WriteLine("on get call debug");
        }

        public async Task<IActionResult> OnPostNavigateToCreateUser()
        {
            return RedirectToPage("ChooseUserType");
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            Debug.WriteLine("on post call debug " + email + " // " + password);
            if (email == null || password == null)
            {
                showErrorMsg = true;
                return null;
            }
            var json = JsonConvert.SerializeObject(new { email = email, password = password });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://www.sistema-tec.somee.com/api/users";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);
            //var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(5) // Sets cookie expiration
            };

            Response.Cookies.Append("email", email, options);
            //return Redirect("https://www.youtube.com");
            if (result == "" ) {
                showIncorrect = true;
                return null;
            }
            showErrorMsg = false;
            showIncorrect = false;
            string CurrentPageUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";
            Debug.WriteLine(CurrentPageUrl);
            //String strPathAndQuery = HttpContext.Request.;
            //String strUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            return RedirectToPage("ApplicationMenu");
        }
    }
}