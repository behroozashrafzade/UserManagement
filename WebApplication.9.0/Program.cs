using Microsoft.EntityFrameworkCore;
using WebApplication._9._0;
using WebApplication._9._0.Dtos;
using WebApplication._9._0.Entities;
using WebApplication._9._0.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddDbContextPool<AppDbContext>(o =>
{
o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDatabase"));
});
builder.Services.AddScoped<IUserService, UserService>();
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

app.MapPost("user/Create",async (IUserService userService,UserCreateParams dto)=>{
    UserResponse result = await userService.Create(dto);
    return Results.Ok(result);
});
app.MapGet("user/Read",async (IUserService userService)=>{
    IEnumerable<UserResponse> result = await userService.Read();
    return Results.Ok(result);
});

app.MapGet("user/Read{id:guid}",async (IUserService userService,Guid id)=>{
    UserResponse? result = await userService.ReadById(id);
   return result==null ? Results.NotFound() : Results.Ok(result);
});

app.MapPut("user/Update",async (IUserService userService,UserUpdateParams param)=>{
    UserResponse? result = await userService.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("user/Delete",async (IUserService userService,Guid id)=>{
    await userService.Delete(id);
    return  Results.Ok();
});


app.Run();

