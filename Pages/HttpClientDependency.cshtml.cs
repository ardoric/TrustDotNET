using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;

namespace TrustDotNEt.Pages
{
    public class HttpClientDependencyModel : PageModel
    {
        public string? HASH;
        private readonly HttpClient _client;
        public HttpClientDependencyModel(HttpClient client)
        {
            _client = client;
        }

        public async Task OnGetAsync()
        {
            /*
             * HttpClient is obtained from dependency injection and includes the trust httpclienthandler.
             */
            var resp = await _client.GetAsync("https://untrusted-root.badssl.com");
            var content = await resp.Content.ReadAsStringAsync();
            using (SHA256 sha = SHA256.Create())
            {
                HASH = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }

        }
    }
}
