using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace LayoutTemplateWebApp.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly IHttpClientFactory _clientFactory;


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }
        private readonly ILogger<IndexModel> _logger;

        /*public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }*/
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
           
        }

        public async Task<RedirectToPageResult> OnGet(string email)
        {

            string id = HttpContext.Session.GetString("email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("Login");

            }
            else
            {
                Email = email;
                Response.Cookies.Append("email", Email);
                return RedirectToPage("ApplicationMenu");
            }
        }
    }

}