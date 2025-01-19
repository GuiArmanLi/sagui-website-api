using athletic.WebAPI.extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();