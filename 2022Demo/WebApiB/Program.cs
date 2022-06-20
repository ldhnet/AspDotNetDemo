using Lee.Hangfire;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5099");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

 
builder.Services.Configure<FormOptions>(options => {
    options.ValueLengthLimit = 209715200;//200MB   //.netcore 限制了每个 POST 数据值的长度为 4M  提升到200M
});

//builder.Services.AddHangfire(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

//app.UseHangfire();


app.Run();
