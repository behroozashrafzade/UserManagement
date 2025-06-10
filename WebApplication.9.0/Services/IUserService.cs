using Microsoft.EntityFrameworkCore;
using WebApplication._9._0.Dtos;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface IUserService
{
    
    Task<IEnumerable<UserResponse>> Read();
    Task<UserResponse?> ReadById(Guid id);
    Task<UserResponse?> Update(UserUpdateParams param);
    Task Delete(Guid id);
    Task<UserResponse> Create(UserCreateParams user);
   
}

public class UserService(AppDbContext dbContext) : IUserService
{
    public async Task<IEnumerable<UserResponse>> Read()
    {
        var list = await dbContext.Users.Select(x=>new UserResponse
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Birthdate = x.Birthdate,
            IsMarried = x.IsMarried,
            Age = 7,
        }).OrderBy(x=>x.Fullname).ToListAsync();
        return list;
    }

    public async Task<UserResponse?> ReadById(Guid id)
    {
        UserEntity? user =await dbContext.Users.FindAsync(id);

        if (user==null)
        {
            return null;
        }
        
        int? age = null;
        if (user.Birthdate!=null)
        {
            age=DateTime.UtcNow.Year - user.Birthdate.Value.Year;
        }

        UserResponse response = new()
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Birthdate = user.Birthdate,
            IsMarried = user.IsMarried,
            Age = 7
        };
        return response;
    }

    public async Task<UserResponse> Update(UserUpdateParams param)
    {
       UserEntity? user =await dbContext.Users.FindAsync(param.Id);
       if (user == null) return null;
       if(param.IsMarried!=null) user.IsMarried = param.IsMarried.Value;
       if (param.PhoneNumber != null) user.PhoneNumber = param.PhoneNumber;
       if (param.Birthdate != null) user.Birthdate = param.Birthdate;
       if (param.Fullname != null) user.Fullname = param.Fullname;
       if (param.Email != null) user.Email = param.Email;
       
       dbContext.Update(user);
       await dbContext.SaveChangesAsync();
       return new UserResponse {
           Id = user.Id,
           Fullname = user.Fullname,
           Email = user.Email,
           PhoneNumber = user.PhoneNumber,
           Birthdate = user.Birthdate,
           IsMarried = user.IsMarried,
           Age = 7
       };

    }

    public async Task Delete(Guid id)
    {
       
        UserEntity? user =await dbContext.Users.FindAsync(id);

        if (user == null) return;
        
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        
            
    }

    public async Task<UserResponse>  Create(UserCreateParams dto)
    {
        UserEntity user=new ()
        {
            Id = Guid.CreateVersion7(),
            Fullname = dto.Fullname,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Birthdate = dto.Birthdate,
            IsMarried = dto.IsMarried,
            
        };
        
        //dbContext.Set<UserEntity>().Add(user);
        var entity=dbContext.Users.Add(user).Entity;
        await dbContext.SaveChangesAsync();

        int? age = null;
        if (entity.Birthdate!=null)
        {
            age=DateTime.UtcNow.Year - entity.Birthdate.Value.Year;
        }
        
        
        return new UserResponse()
        {
            Id = entity.Id,
            Fullname = entity.Fullname,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Birthdate = entity.Birthdate,
            IsMarried = entity.IsMarried,
            Age = age
            
        };
    }
}

