using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //testing
            //var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(sp => new HttpClient 
            //                    { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) })
            //                .AddStaticWebAppsAuthentication();

            //await builder.Build().RunAsync();


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
               
                builder.Configuration.Bind("Cognito", options.ProviderOptions);
                var a =options.ProviderOptions.Authority;
                options.ProviderOptions.DefaultScopes.Add("email");
                if (builder.HostEnvironment.IsDevelopment())
                {
                   options.ProviderOptions.RedirectUri = "https://localhost:5001/authentication/login-callback";
                }
                Console.WriteLine($"Rajan options.ProviderOptions.Authority:{a}");
                
                

            });
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin();  //set the allowed origin  
                    });
            });

            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}
