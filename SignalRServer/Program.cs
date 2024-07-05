using SignalRServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<ChatService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
app.UseCors();

app.MapHub<NotificationHub>("/notificationhub");

app.Run();