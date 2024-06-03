using Microsoft.AspNetCore.SignalR;
using SignalRServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", p => p
        .WithOrigins("null")
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();
app.UseCors("AllowAnyOrigin");

app.MapHub<NotificationHub>("/notificationhub");

app.Run();