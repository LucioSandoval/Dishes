using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        [Display(Name = "Put your dish name here")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chef is required")]
        [MinLength(2, ErrorMessage = "Chef must be at least 2 characters")]
        public string Chef { get; set; }

        [Required(ErrorMessage = "Tastiness is required")]
        [Range(1, 5, ErrorMessage = "Tastiness must be between 1 and 5")]
        [Display(Name = "Put your tastiness level here")]
        public int Tastiness { get; set; }

        [Required(ErrorMessage = "Calories is required")]
        [Range(1, 10000, ErrorMessage = "Calories must be greater than 0")]
        public int Calories { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters")]

        [Display(Prompt = "Description")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}