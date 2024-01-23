using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RecipeBackEnd.Core.Models;

namespace RecipeBackEnd.APIs.Dto
{
    public class RecipeToReturnDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Steps { get; set; }
        public string? Image { get; set; }
        public int Rate { get; set; }

        //////////relation Between Recipe and Category of Recipe
        public int recipeTypeId { get; set; }        /// foreign key
    }
}
