using System.ComponentModel.DataAnnotations;

namespace WebApplication._9._0.Dtos;

public class UserCreateParams
{

    [Required]
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
}

public class UserResponse
{
    public required Guid Id { get; set; }
    public required string Fullname { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime? Birthdate { get; set; }
    public bool IsMarried { get; set; } 
    public int? Age { get; set; }
}
public class UserUpdateParams
{
    [Required]
    public required Guid Id { get; set; }
    
    public  string? Fullname { get; set; }
    
    public  string? Email { get; set; }
     
   
    public  string? PhoneNumber { get; set; }
    
    public DateTime? Birthdate { get; set; }

    public bool? IsMarried { get; set; } 
}

