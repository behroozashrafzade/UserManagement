using Microsoft.EntityFrameworkCore;
using WebApplication._9._0;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContextPool<AppDbContext>(o =>
{
o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapGet("/hello", () => "Hello World!");
app.MapGet("/Hi", (string name) => "Hello "+name+"!");
app.MapPost("Welcome",(string name) =>"Welcome "+name+"!" );

app.Run();

