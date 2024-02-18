using System.Net;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.WebHost.ConfigureKestrel(webBuilder =>
    webBuilder.Listen(IPAddress.Any, builder.Configuration.GetValue("RestPort", 8080))
);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

app.MapReverseProxy();
app.Run();