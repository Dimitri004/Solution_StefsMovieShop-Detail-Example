using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StefsMovieShop.Client.Interfaces;
using StefsMovieShop.Client.Repos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StefsMovieShop.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //Add Services
            builder.Services.AddApiAuthorization();

            //Named Client
            builder.Services
                .AddHttpClient("Secured", c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services
                .AddHttpClient("NotSecured", c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            //Add Repos
            builder.Services.AddScoped<IMoviesRepo, MoviesRepo>();

            //Run Build
            await builder.Build().RunAsync();
        }
    }
}
