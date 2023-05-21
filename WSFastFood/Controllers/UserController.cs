using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Responses;
using WSFastFood.Services.UsersServices;

namespace WSFastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            GeneralResponse response = new();
            response = await _userService.SearchUsers();

            if (response.Success == 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            GeneralResponse response = new();
            response = await _userService.SearchUser(id);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(NewUserDto newUserDto)
        {
            GeneralResponse response = new();
            if (!ModelState.IsValid)
            {
                return BadRequest(response);
            }

            response = await _userService.AddUser(newUserDto);

            if (response.Success == 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(EditUserDto editUserDto)
        {
            GeneralResponse response = new();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            response = await _userService.EditUser(editUserDto);
            if (response.Success == 0)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
                
    }
}
