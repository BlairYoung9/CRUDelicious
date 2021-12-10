using System;
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage =" Minimum length 2 characters!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Chef Name is required!")]
        public string Chef { get; set; }
        [Required(ErrorMessage = "Tastiness is required!")]
        public int Tastiness { get; set; }
        [Required(ErrorMessage = "Calories is required!")]
        public int Calories {get; set;}
        [Required(ErrorMessage = "Description is required!")]
        public string Description {get;set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}