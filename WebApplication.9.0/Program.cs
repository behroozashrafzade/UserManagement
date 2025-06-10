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
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
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
app.MapPost("schoo/Create",async (ISchoolService Service,UserCreateParams dto)=>{
    SchoolEntitiy result = await Service.Create(dto);
    return Results.Ok(result);
});
app.MapGet("user/Read",async (IUserService userService)=>{
    IEnumerable<UserResponse> result = await userService.Read();
    return Results.Ok(result);
});
app.MapGet("user/Read",async (ISchoolService Service)=>{
    IEnumerable<SchoolEntitiy> result = await Service.Read();
    return Results.Ok(result);
});

app.MapGet("user/Read{id:guid}",async (IUserService userService,Guid id)=>{
    UserResponse? result = await userService.ReadById(id);
   return result==null ? Results.NotFound() : Results.Ok(result);
   
});app.MapGet("School/Read{id:guid}",async (ISchoolService Service,Guid id)=>{
    SchoolEntitiy? result = await Service.ReadById(id);
   return result==null ? Results.NotFound() : Results.Ok(result);
});

app.MapPut("user/Update",async (IUserService userService,UserUpdateParams param)=>{
    UserResponse? result = await userService.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
    
});app.MapPut("School/Update",async (ISchoolService Service,UserUpdateParams param)=>{
    SchoolEntitiy? result = await Service.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("user/Delete",async (IUserService userService,Guid id)=>{
    await userService.Delete(id);
    return  Results.Ok();
});
app.MapDelete("School/Delete",async (ISchoolService Service,Guid id)=>{
    await Service.Delete(id);
    return  Results.Ok();
});

app.Run();

