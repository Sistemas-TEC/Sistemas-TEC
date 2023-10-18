using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LayoutTemplateWebApp.Pages.AppCards
{
    public class CancherksModel : PageModel
    {
        public ActionResult OnGet()
        {
            string email= Request.Cookies["email"];
            string url = "http://www.cancherks.somee.com/";

            return Redirect(url+email);
        }
    }
}
