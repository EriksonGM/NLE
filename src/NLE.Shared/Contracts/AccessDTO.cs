using System.ComponentModel.DataAnnotations;

namespace NLE.Shared.Contracts;

public class AccessDTO
{
    public Guid Id { get; set; }
    
    public DateTime EventDate { get; set; }
    
    public string HttpMethod { get; set; }
    
    public int StatusCode { get; set; }
    
    public long Length { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Url { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string RemoteAddress { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string SentTo { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Client { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string Agent { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Host { get; set; }
    
    [Required]
    public string Referer { get; set; }
}