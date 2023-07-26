global using PhoneBook.Client.Services.ContactService;
global using PhoneBook.Shared;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhoneBook.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("PhoneBook.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PublicContactService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

//Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("PhoneBook.ServerAPI"));
builder.Services.AddScoped<IPrivateContactService, PrivateContactService>();

builder.Services.AddApiAuthorization();

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
