
using Grpc.Net.Client;
using MudBlazor.Services;
using SimpleTrading.SettingsReader;
using TradeLog;

var builder = WebApplication.CreateBuilder(args);

// var settings = new SettingsModel
// {
//     TradeLogGrpc = "http://127.0.0.1:32011",
// };

var settingsModel = SettingsReader.ReadSettings<SettingsModel>(".settings.yaml");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton(
    new TradeLogGrpcService.TradeLogGrpcServiceClient(
        GrpcChannel.ForAddress(settingsModel.TradeLogGrpc)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();