using System.ComponentModel.DataAnnotations;

namespace NLE.Domain.Entities;

public class FileLog
{
    [Key]
    public Guid FileLogId { get; set; }
    
    public DateTime ReadedAt { get; set; }
    
    [Required]
    public string FileName { get; set; }
    
    public int LastLine { get; set; }
}