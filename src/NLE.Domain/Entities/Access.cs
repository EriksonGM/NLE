using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace NLE.Data.Entities;

public class Access
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime EventDate { get; set; }
    
    public int HttpMethodId { get; set; }
    
    [ForeignKey("HttpMethodId")]
    public HttpMethod HttpMethod { get; set; }
    
    public Guid HostId { get; set; }
    
    [ForeignKey("HostId")]
    public Host Host { get; set; }
    
    [ForeignKey("ClientId")]
    public Client Client { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public string Agent { get; set; }
    
    public int StatusCode { get; set; }
    
    public long Length { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string RemoteAddress { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string SentTo { get; set; }
    
    public int? ClientId { get; set; }
    
    
    
    
}