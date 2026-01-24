using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PoliNote.Pages
{
    public class ActivateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Token { get; set; } = string.Empty;

        public void OnGet()
        {
        }
    }
}
