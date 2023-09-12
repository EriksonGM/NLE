using System.ComponentModel.DataAnnotations;

namespace NLE.Domain.Entities;

public class Host
{
    [Key]
    public Guid HostId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Address { get; set; }
}