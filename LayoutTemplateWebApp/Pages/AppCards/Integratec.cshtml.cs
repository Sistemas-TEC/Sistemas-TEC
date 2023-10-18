using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LayoutTemplateWebApp.Pages.AppCards
{
    public class IntegratecModel : PageModel
    {
        public ActionResult OnGet()
        {

            string email = Request.Cookies["email"];
            if (email.Contains("@estudiantec.cr"))
            {
                string url = "http://sama.somee.com/";

                return Redirect(url + email);
            }
            else
            {
                return null;
            }
        }
    }
}
