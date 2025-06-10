using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication._9._0.Entities;

[Table("Classes")]
public class ClassEntity
{
    [Key] 
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Subject { get; set; }

    public Guid? SchoolId { get; set; }
    public SchoolEntitiy School { get; set; }
    public IEnumerable<UserEntity> Users { get; set; }
}