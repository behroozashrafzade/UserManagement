using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication._9._0.Entities;


[Table("schools")]
public class SchoolEntitiy
{
    [Key] 
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public IEnumerable<ClassEntity>? Classes { get; set; }
}