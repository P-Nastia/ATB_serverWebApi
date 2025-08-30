using Core.Interfaces;
using Core.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace ATB_serverWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var model = await categoryService.List();

        return Ok(model);
    }
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] CategoryCreateModel model)
    {
        var category = await categoryService.Create(model);
        return Ok(category);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var model = await categoryService.GetItemById(id);

        if (model == null)
        {
            return NotFound();
        }
        return Ok(model);
    }
    [HttpPut("edit")] 
    public async Task<IActionResult> Edit([FromForm] CategoryEditModel model)
    {
        var category = await categoryService.Edit(model);

        return Ok(category);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await categoryService.Delete(id);
        return Ok();
    }
}