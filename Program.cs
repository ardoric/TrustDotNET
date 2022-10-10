using System.Net;
using System.Net.Security;

/*
 * Add handler code to ServicePointManager
 * This works for older versions of .NET (before 2.1) and for older APIs (e.g. WebRequest)
 * This DOES NOT work for HttpClient on newer .NET Core
 * 
 * 7890C8934D5869B25D2F8D0D646F9A5D7385BA85 is the thumbprint for the root authority of https://untrusted-root.badssl.com/
 */
ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) =>
{
    if (error == SslPolicyErrors.None)
        return true;

    if (error == SslPolicyErrors.RemoteCertificateChainErrors)
        return chain.ChainElements[^1].Certificate.Thumbprint == "7890C8934D5869B25D2F8D0D646F9A5D7385BA85".ToUpper();

    return false;
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

