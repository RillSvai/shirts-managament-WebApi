﻿using ShirtsManagament.API.Models.Validations.ShirtAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShirtsManagament.API.Models
{
    [CorrectSize]
    public class Shirt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Brand { get; set; }
        public string? Color { get; set; }
        [Required]
        [Range(6,12)]
        public double Size { get; set; }
        [Required]
        [Gender]
        public char Gender {  get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}