using Framework.Utility.Config;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5199");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddLocalization();


builder.Services.AddMemoryCache();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
GlobalConfig.ServiceProvider = app.Services;
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

