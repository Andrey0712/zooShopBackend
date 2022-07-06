
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebZooShop.Data.Entities;
using WebZooShop.Model;
using WebZooShop.Data;

namespace WebZooShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;
        public CategoryController(IMapper mapper, AppEFContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Список категорій
        /// </summary>
        /// <returns>Повертає лист</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List categorys</response>
        /// <response code="400">List categorys has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  categorys list right now</response>

        [HttpGet("list")]
        public async Task<IActionResult> Category()
        {
            Thread.Sleep(2000);
            var list = await _context.Categories.Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .AsQueryable().ToListAsync();

            return Ok(list);
        }

        /// <summary>
        /// Додати категорію
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає id</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Add category</response>
        /// <response code="400">Add category has missing/invalid values</response>
        /// <response code="500">Oops! Can't add category right now</response>
        /// 
        [HttpPost("create")]
        //[Authorize]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            CategoryEntity category = _mapper.Map<CategoryEntity>(model);
            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok(new { id = category.Id });
        }

        /// <summary>
        /// Видалення категорії
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає messege</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Delete category</response>
        /// <response code="400">Delete category has missing/invalid values</response>
        /// <response code="500">Oops! Can't delete category now</response>
        /// 
        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] CategoryDelViewModel model)
        {

            var res = _context.Categories.FirstOrDefault(x => x.Id == model.Id);
            if (res == null)
            {
                return BadRequest(new { message = "Check id!" });
            }

            _context.Categories.Remove(res);
            _context.SaveChanges();
            return Ok(new { message = "Category deleted" });
        }
    }
}
