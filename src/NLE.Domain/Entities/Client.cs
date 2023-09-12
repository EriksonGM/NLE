using System.ComponentModel.DataAnnotations;

namespace NLE.Domain.Entities;

public class Client
{
    [Key]
    public int ClientId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}