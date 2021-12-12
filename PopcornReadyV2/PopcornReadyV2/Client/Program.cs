using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using PopcornReadyV2.Shared.Clients;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PopcornReadyV2.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var apiUri = new Uri(builder.HostEnvironment.BaseAddress);

            builder.Services.AddHttpClient("PopcornReadyV2.ServerAPI", client => client.BaseAddress = apiUri)
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PopcornReadyV2.ServerAPI"));

            // configures the necessary services for auth and using the javascript services
            builder.Services.AddApiAuthorization();

            builder.Services.AddMudServices().AddMudBlazorSnackbar(cfg =>
            {
                cfg.ClearAfterNavigation = false;
                cfg.ShowCloseIcon = true;
                cfg.PreventDuplicates = false;
            });

            builder.Services.AddRefitClient<IPopcornUnAuthClient>()
                .ConfigureHttpClient(x => x.BaseAddress = apiUri);

            builder.Services.AddRefitClient<IPopcornAuthClient>()
               .ConfigureHttpClient(x => x.BaseAddress = apiUri)
               .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            await builder.Build().RunAsync();
        }
    }
}
