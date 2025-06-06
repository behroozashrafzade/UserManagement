using WebApplication._9._0.Dtos;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface IUserService
{
    
    Task<IEnumerable<UserResponse>> Read();
    Task<UserResponse> ReadById(Guid i);
    Task<UserResponse> Update();
    Task<UserResponse> Create(UserCreateParams user);
   
}

public class UserService(AppDbContext dbContext) : IUserService
{
   
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
            Fullname = entity.Fullname,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Birthdate = entity.Birthdate,
            IsMarried = entity.IsMarried,
            Age = age
            
        };
    }
}

