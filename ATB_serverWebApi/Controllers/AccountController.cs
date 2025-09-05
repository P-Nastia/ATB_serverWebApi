using AutoMapper;
using Core.Interfaces;
using Core.Models.Account;
using Core.Services;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ATB_serverWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController(IMapper mapper,
    UserManager<UserEntity> userManager,
    IImageService imageService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        var user = mapper.Map<UserEntity>(model);

        user.Image = await imageService.SaveImageAsync(model.ImageFile!);

        var result = await userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return Ok(new
            {
                status = 200,
                isValid = true,
                message = "Registration successful"
            });
        }
        else
        {
            return BadRequest(new
            {
                status = 400,
                isValid = false,
                errors = "Registration failed"
            });
        }

    }
}