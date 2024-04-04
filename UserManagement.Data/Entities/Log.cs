using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace UserManagement.Models;
public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long UserId { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = default!;
    public string LogDescription { get; set; } = default!;
}
