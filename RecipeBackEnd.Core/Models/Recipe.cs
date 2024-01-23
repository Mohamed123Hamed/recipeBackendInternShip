using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Core.Models
{
    public class Recipe 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]

        [MinLength(3, ErrorMessage = "The Minimum Lenght More Than 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "The Minimum Lenght More Than 1 characters")]
        public string Ingredients { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "The Minimum Lenght More Than 1 characters")]
        public string Steps { get; set; }
        public string? Image { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }

        // Relation Between Recipe and Type of Recipe
        [ForeignKey("recipeTypeId")]
        public int recipeTypeId { get; set; }   /// foreign key
        public RecipeType recipeType { get; set; }    // contain name of type

    }
}
