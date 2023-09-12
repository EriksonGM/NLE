using System.ComponentModel.DataAnnotations;

namespace NLE.Domain.Entities;

public class HttpMethod
{
    [Key]
    public int HttpMethodId { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string Name { get; set; }
}