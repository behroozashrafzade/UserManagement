using Microsoft.EntityFrameworkCore;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0.Services;

public interface ISchoolService
{
    Task<List<SchoolEntitiy>> Read();
    Task<SchoolEntitiy?> ReadById(Guid i);
    Task<SchoolEntitiy?> Update(SchoolEntitiy param);
    Task Delete(Guid id);
    Task<SchoolEntitiy> Create(SchoolEntitiy param);
}

public class SchoolService (AppDbContext dbContext) : ISchoolService
{
    public async Task<List<SchoolEntitiy>> Read()
    {
        List<SchoolEntitiy> list = await dbContext.School.ToListAsync();
        
        return list;
    }

    public async Task<SchoolEntitiy?> ReadById(Guid id)
    {
       SchoolEntitiy? e=await dbContext.School.FindAsync(id);

       return e ?? null;
    }

    public async Task<SchoolEntitiy?> Update(SchoolEntitiy param)
    {
        SchoolEntitiy? e = await dbContext.School.FindAsync(param.Id);
        if (e == null) return null;
        e.Title = param.Title;

        dbContext.School.Update(e);
        await dbContext.SaveChangesAsync();
        return new SchoolEntitiy() {
            Id = e.Id,
            Title = e.Title,
        };
    }

    public async Task Delete(Guid id)
    {
        SchoolEntitiy? e = await dbContext.School.FindAsync(id);
        if( e == null) return;
        dbContext.School.Remove(e);
        await dbContext.SaveChangesAsync();
    }

    public async Task<SchoolEntitiy> Create(SchoolEntitiy param)
    {
        SchoolEntitiy e = new()
        {
            Id = Guid.CreateVersion7(),
            Title = param.Title,
        };
        SchoolEntitiy entity =  dbContext.School.Add(e).Entity;
        await dbContext.SaveChangesAsync();
        return entity;
    }
}