using Lee.Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5099");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(builder.Configuration);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseHangfire();


app.Run();
