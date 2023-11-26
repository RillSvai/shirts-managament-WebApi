using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShirtsManagement.Web.Models.Validations.ShirtAttributes;

namespace ShirtsManagement.Web.Models
{
    public class Shirt
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        public required string Brand { get; set; }
        public string? Color { get; set; }
        [Required]
        [Range(6,12)]
        [CorrectSize]
        public double Size { get; set; }
        [Required]
        [Gender]
        public char Gender {  get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(50,10000)]
        public decimal Price { get; set; }
    }
}
