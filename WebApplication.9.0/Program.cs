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

#region User

//     ---> Create
app.MapPost("user/Create",async (IUserService userService,UserCreateParams dto)=>{
    UserResponse result = await userService.Create(dto);
    return Results.Ok(result);
});

//     ---> Read
app.MapGet("user/Read",async (IUserService userService)=>{
    IEnumerable<UserResponse> result = await userService.Read();
    return Results.Ok(result);
});

//     ---> Read By Id
app.MapGet("user/Read{id:guid}",async (IUserService userService,Guid id)=>{
    UserResponse? result = await userService.ReadById(id);
    return result==null ? Results.NotFound() : Results.Ok(result);
});
//     ---> Delete
app.MapDelete("user/Delete",async (IUserService userService,Guid id)=>{
    await userService.Delete(id);
    return  Results.Ok(); 
       
});
//     ---> Update
app.MapPut("user/Update",async (IUserService userService,UserUpdateParams param)=>{
    UserResponse? result = await userService.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
   
});

#endregion



#region class
//     ---> Create

app.MapPost("Class/Create",async (IClassService Service,ClassEntity dto)=>{
    ClassEntity result = await Service.Create(dto);
    return Results.Ok(result);
});
//     ---> Read
app.MapGet("Class/Read",async (IClassService Service)=>{
   IEnumerable<ClassEntity> result = await Service.Read();
    return Results.Ok(result);
});
//     ---> Read By Id
app.MapGet("Class/Read{id:guid}",async (IClassService Service,Guid id)=>{
    ClassEntity? result = await Service.ReadById(id);
    return result==null? Results.NotFound() : Results.Ok(result);
});
//     ---> Delete
app.MapDelete("Class/Delete",async (IClassService Service,Guid id)=>{
    await Service.Delete(id);
    return  Results.Ok(); 
       
});
//     ---> Update
app.MapPut("Class/Update",async (IClassService Service,ClassEntity param)=>{
    ClassEntity? result = await Service.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
   
});
#endregion

#region School
//     ---> Read 
app.MapGet("School/Read{id:guid}",async (ISchoolService Service,Guid id)=>{
    SchoolEntitiy? result = await Service.ReadById(id);
   return result==null ? Results.NotFound() : Results.Ok(result);
});
       
//     ---> Update 
app.MapPut("School/Update",async (ISchoolService Service,SchoolEntitiy param)=>{
    SchoolEntitiy? result = await Service.Update(param);
    return result==null ? Results.NotFound() : Results.Ok(result);
});

//     ---> Create 
app.MapPost("school/Create",async (ISchoolService Service,SchoolEntitiy param)=>{
        SchoolEntitiy result = await Service.Create(param);
        return Results.Ok(result);
});
 
//     ---> Delete 
app.MapDelete("School/Delete",async (ISchoolService Service,Guid id)=>{
    await Service.Delete(id);
    return  Results.Ok();
});


//     ---> Read By Id
app.MapGet("School/Read{id:guid}",async (ISchoolService Service,Guid id)=>{
    SchoolEntitiy? result = await Service.ReadById(id);
    return result==null ? Results.NotFound() : Results.Ok(result);
});
#endregion


app.Run();








