using RecipeBackEnd.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Core.IRepo
{
    public interface IRecipeBackEnd
    {
        Task<List<Recipe>> GetAll();
        Task<Recipe> GetById(int Id);
        Task Add(Recipe recipe);
        Task<Recipe> Edite(Recipe recipe);
        Task Delete(int id);
        Task<List<Recipe>> Paging(int PagNum, int PagSize);
        Task<List<Recipe>> SearchByNameOrIngerdent(string Value);
        Task<List<Recipe>> SearchByNameAndIngerdent(string name, string ingredent);
    }
}
