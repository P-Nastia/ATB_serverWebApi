using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATB_serverWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCategories()
        {
            var категорії = new[]
            {
                new { Id = 1, Name = "Напої безалкогольні", Image = "https://src.zakaz.atbmarket.com/cache/category/Безалкогольні напої.webp" },
                new { Id = 2, Name = "Овочі та фрукти", Image = "https://src.zakaz.atbmarket.com/cache/category/Овочі та фрукти.webp" },
                new { Id = 3, Name = "Морозиво", Image = "https://src.zakaz.atbmarket.com/cache/category/334-morozivo.webp" },
                new { Id = 4, Name = "Заморожені продукти", Image = "https://src.zakaz.atbmarket.com/cache/category/Заморожені вироби.webp" }
            };

            return Ok(категорії);
        }
    }
}
