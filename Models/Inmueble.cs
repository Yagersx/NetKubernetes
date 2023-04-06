using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetKubernates.Models;

public class Inmueble
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    
    [Required]
    public string? Address { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Phone { get; set; }

    [Required]
    [Column(TypeName="decimal(18,4)")]
    public decimal Price { get; set; }
    
    [Required]
    public string? Image { get; set; }

    [Required]
    public DateTime? CreationDate { get; set; }
    
    public Guid? UserId { get; set; }
}