using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface IClassService
{
    Task<List<ClassEntity>> Read();
    Task<ClassEntity?>ReadById(Guid i);
    Task<ClassEntity?> Update(ClassEntity param);
    Task Delete(Guid id);
    Task<ClassEntity?> Create(ClassEntity param);

}

public class ClassService (AppDbContext dbContext): IClassService
{
    public async Task<List<ClassEntity>> Read()
    {
        List<ClassEntity> list = await dbContext.Class.ToListAsync();
        return list;
    }

    public async Task<ClassEntity?> ReadById(Guid id)
    {
       ClassEntity ? e = await dbContext.Class.FindAsync(id);
       return e ?? null;
    }

    public async Task<ClassEntity?> Update(ClassEntity param)
    {
       ClassEntity? e = await dbContext.Class.FindAsync(param.Id);
       if(e == null) return null;
       e.Title=param.Title;
       e.Subject=param.Subject;
       if(param.SchoolId != null) e.SchoolId=param.SchoolId;
       dbContext.Class.Update(e);
       await dbContext.SaveChangesAsync();
       return new ClassEntity
       {
           Id = e.Id,
           Title = e.Title,
           Subject = e.Subject,
           SchoolId = e.SchoolId
           
       };
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ClassEntity?> Create(ClassEntity param)
    {
        ClassEntity e = new()
        {
            Id = Guid.NewGuid(),
            Subject = param.Subject,
            Title = param.Title,
            SchoolId = param.SchoolId
        };
        ClassEntity entity =  dbContext.Class.Add(e).Entity;
        await dbContext.SaveChangesAsync();
        return entity;
    }
}