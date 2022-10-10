using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;

namespace TrustDotNEt.Pages
{
    public class HttpClientErrorModel : PageModel
    {
        public string HASH;

        private readonly HttpClientHandler _httpClientHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (msg, cert, chain, error) =>
            {
                if (error == SslPolicyErrors.None)
                    return true;

                if (error == SslPolicyErrors.RemoteCertificateChainErrors)
                    return chain.ChainElements[^1].Certificate.Thumbprint == "7890C8934D5869B25D2F8D0D646F9A5D7385BA85".ToUpper();

                return false;
            }
        };
        public async Task OnGetAsync()
        {
            // will not succeed because this API does not use ServicePointManager and this call is not using the handler above
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("https://untrusted-root.badssl.com");
            var content = await resp.Content.ReadAsStringAsync();
            using (SHA256 sha = SHA256.Create())
            {
                HASH = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(content)));
            }

        }
    }
}
