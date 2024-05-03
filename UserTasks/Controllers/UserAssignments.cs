using Microsoft.AspNetCore.Mvc;
using UserTasks.Domain.Models;
using UserTasks.Infrastructure.Services.Interfaces;

namespace UserTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAssignments : Controller
    {
        public readonly IUserAssignmentService _userAssignmentService;

        public UserAssignments(IUserAssignmentService userAssignmentService)
        {
            _userAssignmentService = userAssignmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var result = await _userAssignmentService.GetUsers();

            return result;
        }

        [HttpPost("user")]
        public async Task<User> CreateUser([FromBody]string userName)
        {
            var result = await _userAssignmentService.AddUser(userName);

            return result;
        }

        [HttpPost("assignment")]
        public async Task<Assignment> CreateAssignment([FromBody] string assignmentDescription)
        {
            var result = await _userAssignmentService.AddAssignment(assignmentDescription);

            return result;
        }
    }
}