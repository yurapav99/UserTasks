using Microsoft.AspNetCore.Mvc;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {
        public readonly IUserService _userService;
        public WeatherForecastController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {

            var res = await _userService.GetUsers();

            return  res.ToList();
        }
    }
}