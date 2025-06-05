using WebApplication._9._0.Dtos;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface IUserService
{
    UserResponse Create(UserCreateParams user);
}

public class UserService(AppDbContext dbContext) : IUserService
{
   
    public UserResponse Create(UserCreateParams dto)
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
        var entity=dbContext.Users.Add(user);
        dbContext.SaveChanges();

        int? age = null;
        if (entity.Entity.Birthdate!=null)
        {
            age=DateTime.UtcNow.Year - entity.Entity.Birthdate.Value.Year;
        }
        
        
        return new UserResponse()
        {
            Fullname = entity.Entity.Fullname,
            Email = entity.Entity.Email,
            PhoneNumber = entity.Entity.PhoneNumber,
            Birthdate = entity.Entity.Birthdate,
            IsMarried = entity.Entity.IsMarried,
            Age = age
            
        };
    }
}

