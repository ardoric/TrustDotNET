using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace TrustDotNEt.Pages
{
    public class WebRequestModel : PageModel
    {
        public string HASH;

        public async Task OnGetAsync()
        {
            // will succeed because this API uses ServicePointManager
            var req = WebRequest.Create("https://untrusted-root.badssl.com");
            var resp = await req.GetResponseAsync();
            var content = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            using (SHA256 sha = SHA256.Create())
            {
                HASH = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }
        }
    }
}
