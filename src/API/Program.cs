using System.Text.Json.Serialization;
using API.extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddDependencyInjectionExtension();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddAuthenticationSetup(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();