using ShirtsManagament.API.Models.Validations.ShirtAttributes;
using System.ComponentModel.DataAnnotations;

namespace ShirtsManagament.API.Models
{
    [CorrectSize]
    public class Shirt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        public string? Color { get; set; }
        [Required]
        [Range(6,12)]
        public double Size { get; set; }
        [Required]
        [Gender]
        public char Gender {  get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
