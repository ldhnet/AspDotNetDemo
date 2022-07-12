using MqttService.Code;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5750");
// https://www.cnblogs.com/weskynet/p/16441219.html 
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHostedService<MqttHostService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
