using System.Reflection;
using GitRCFS;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using TerraceApi.Services;
using TerraceApi.Services.AboutMe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Encodeous - Public API",
        Description = "This API is open to the public and can be used by anyone. It contains some of the functionality of my personal website, but also has useful features. Please note, your access to this API may be revoked if you abuse it."
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin",
        cors =>
        {
            cors.AllowAnyOrigin();
            cors.AllowAnyHeader();
        });
});
services.AddMemoryCache();
services.AddHttpClient();
services.AddSingleton<SkiaService>();
services.AddTransient<GithubService>();
services.AddTransient<AmRenderService>();

builder.Host.UseSystemd();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors("AnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();