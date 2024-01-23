using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RecipeBackEnd.APIs.Dto;
using RecipeBackEnd.Core.IRepo;
using RecipeBackEnd.Core.Models;
using RecipeBackEnd.Repository;

namespace RecipeBackEnd.APIs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeBackEnd _recipeInterface;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeBackEnd recipeInterface, IMapper mapper)
        {
            _recipeInterface = recipeInterface;
            _mapper = mapper;
        }

        [HttpGet]                                    // Get :  /api/Recipe/GetAll
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _recipeInterface.GetAll());
        }

        [HttpGet("{id}")]                           // Get :  /api/Recipe/Get/1
        public async Task<ActionResult<RecipeToReturnDto>> Get(int id)
        {
            if (id == 0)
                return BadRequest("Id invalid");
            var Result = await _recipeInterface.GetById(id);
            if (Result == null)
                return BadRequest("Recipe Not Found");
            return Ok(Result);
        }

        [HttpPost]                                // Post : api/Recipe/Add
        public async Task<IActionResult> Add(RecipeToReturnDto recipe)
        {
            if (recipe == null) return BadRequest("please add Recipe with values");
            var result = _mapper.Map<RecipeToReturnDto, Recipe>(recipe);
            await _recipeInterface.Add(result);
            return Ok(result);
        }

        [HttpPut]                                // Put : /api/Recipe/Edite
        public async Task<IActionResult> Edite(RecipeToReturnDto recipe)
        {   if (recipe == null) return BadRequest("Invaled Recip");
            var value = _mapper.Map<RecipeToReturnDto, Recipe>(recipe);
            var Result = await _recipeInterface.Edite(value);
            return Ok(Result);
        }

        [HttpDelete("{id}")]                      // Delete : /api/Recipe/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest("Recipe Invaled Id");
            if (await _recipeInterface.GetById(id) == null)
                return BadRequest("Not Found");
            await _recipeInterface.Delete(id);
            return Ok();
        }

        //Pagination
        [HttpGet("{pageSize}")]         // Get : api/Recipe/GetPagedRecipes/3?pageNumber=1
        public async Task<IActionResult> GetPagedRecipes(int pageNumber = 1, int pageSize = 3)
        {
            return Ok(await _recipeInterface.Paging(pageNumber, pageSize));
        }

        //Search by value [Name OR Ingeredent]
        [HttpPost("{Value}")]                     //Post : /api/Recipe/searchValue/k
        public async Task<IActionResult> SearchValue(string value)
        {
            return Ok(await _recipeInterface.SearchByNameOrIngerdent(value));
        }

        //Search by value [Name, Ingeredent]
        [HttpPost]                       //Get : /api/Recipe/GetAllRecipeByValue/{--, --}
        public async Task<IActionResult> GetAllRecipeByValue(string name, string inget)
        {
            return Ok(await _recipeInterface.SearchByNameAndIngerdent(name, inget));
        }
    }

}
