using AutoMapper;
using Core.Constants;
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
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        var token = await accountService.Register(model);
        if (token == null)
        {
            return BadRequest(new
            {
                status = 400,
                isValid = false,
                errors = "Registration failed"
            });
        }
        else
        {
            return Ok(new { Token = token });
        }
    }
}