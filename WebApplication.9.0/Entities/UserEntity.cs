using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication._9._0.Entities;
[Table("Users")]
public class UserEntity
{
    [Key]
    public Guid Id { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public required string Fullname { get; set; }
    [EmailAddress]
     public required string Email { get; set; }
     
    [Required]
    [MinLength(10)]
    [MaxLength(12)]
    public required string PhoneNumber { get; set; }
    
    public DateTime? Birthdate { get; set; }

    public bool IsMarried { get; set; } = false;

    public IEnumerable<ClassEntity> classes { get; set; }

}