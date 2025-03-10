using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Dto;
using Services.Interfaces;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService homeService;
        public HomeController(IHomeService _homeService)
        {
            homeService = _homeService;
        }

        [HttpPost("CreateArticle")]
        public async Task<IActionResult> CreateArticle(ArticleDto articleDto)
        {
            if (articleDto == null) return BadRequest(new {success = false, message = "can not create article with empty fields"});
            if (ModelState.IsValid)
            {
                var createArticle = await homeService.CreateArticle(articleDto);
                if (!createArticle) return BadRequest(new { success = false, message = "Internal server error" });
                return Ok(new { success = true, message = "create article successful" });
            }
            ModelState.AddModelError("", "Invalid Model");
            return BadRequest(new { success = false, message = "Invalid Model" });
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest(new { success = false, message = "can not create category with empty fields" });
            if (ModelState.IsValid)
            {
                var createCategory = await homeService.CreateCategory(categoryDto);
                if (!createCategory) return BadRequest(new { success = false, message = "Internal server error" });
                return Ok(new { success = true, message = "create category successful" });
            }
            ModelState.AddModelError("", "Invalid Model");
            return BadRequest(new { success = false, message = "Invalid Model" });
        }

        [HttpGet("AllArticle")]
        public async Task<IActionResult> GetAllArticles()
        {
            var articles = await homeService.GetAllArticles();
            if (articles == null) return BadRequest(new { success = false, message = "No article found" });
            return Ok(new { success = true, message = "articles fetched successfully", result = articles });
        }
    }
}
