using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhoneBook.Web;
using PhoneBook.Web.Services;
using PhoneBook.Web.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7142/") });
builder.Services.AddScoped<IContactService, ContactService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
