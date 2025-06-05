using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface IUserService
{
    UserEntity Create(UserEntity user);
}

public class UserService(AppDbContext dbContext) : IUserService
{
   
    public UserEntity Create(UserEntity user)
    {
        //dbContext.Set<UserEntity>().Add(user);
        var entity=dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return entity.Entity;
    }
}

