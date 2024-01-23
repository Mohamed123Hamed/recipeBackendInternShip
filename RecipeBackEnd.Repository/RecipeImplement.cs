using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using RecipeBackEnd.Core.IRepo;
using RecipeBackEnd.Core.Models;
using RecipeBackEnd.Repository.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace RecipeBackEnd.Repository
{
    public class RecipeImplement : IRecipeBackEnd
    {
        private readonly StoreContext _dbcontext;
        public RecipeImplement(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<List<Recipe>> GetAll()
        {
            List<Recipe> result = await _dbcontext.Recipes
                                       .Include(a => a.recipeType)
                                       .OrderByDescending(r => r.Rate)
                                       .AsNoTracking()
                                       .ToListAsync();
            return result;
        }
        public async Task<Recipe> GetById(int Id)
        {
            Recipe result = await _dbcontext.Recipes
                            .Include(a => a.recipeType)
                            .FirstOrDefaultAsync(p => p.ID == Id);
            return result;
        }
        public async Task Add(Recipe recipe)
        {
            await _dbcontext.Recipes.AddAsync(recipe);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<Recipe> Edite(Recipe recipe)
        {
            var checkid = await _dbcontext.Recipes
                                .FirstOrDefaultAsync(a => a.ID == recipe.ID);
            if (checkid != null)
            {
                _dbcontext.Recipes.Update(checkid);
                _dbcontext.Entry(checkid).CurrentValues.SetValues(recipe);
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return checkid;
        }

        public async Task Delete(int id)
        {
            if (id != 0)
            {
                var deleteRecipe =await _dbcontext.Recipes.FindAsync(id);
                _dbcontext.Recipes.Remove(deleteRecipe);
                _dbcontext.SaveChanges();
            }
        }
        public async Task<List<Recipe>> Paging(int pageNumberr = 1, int pageSizee = 3)
        {
            var items = await _dbcontext.Recipes.Skip((pageNumberr - 1) * pageSizee)
                        .Take(pageSizee).ToListAsync();
            return items;
        }

        public async Task<List<Recipe>> SearchByNameOrIngerdent(string Value)
        {
            var result = _dbcontext.Recipes
                        .Include(a => a.recipeType).AsNoTracking(); // high performance forget data
            if (!string.IsNullOrEmpty(Value))
            {
                result = result.Where(x => x.Name.ToLower().Contains(Value) ||
                                           x.Ingredients.ToLower().Contains(Value));
            }
            return (await result.ToListAsync());
        }

        public Task<List<Recipe>> SearchByNameAndIngerdent(string name, string ingredent)
        {
            var result = _dbcontext.Recipes
                         .Include(a => a.recipeType).AsNoTracking(); // high performance forget data
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.Name.ToLower().Contains(name));
            }
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.Ingredients.ToLower().Contains(ingredent));
            }
            return result.ToListAsync();
        }
    }
}