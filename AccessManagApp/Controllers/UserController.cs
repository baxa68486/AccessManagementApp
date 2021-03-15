using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestApp.Interfaces;
using System.Linq;

namespace AccessManagApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<User> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(ILogger<User> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("", Name = "GetUsers")]
        public async Task<IActionResult> GetUsers(int? page, int? size)
        {
            if (page == null && size == null)
            {
                var users = await _userService.FindAllAsync();
                var dtos = _mapper.Map<List<UserDTO>>(users.ToList());
                return Ok(dtos);
            }
            
            if (page == null || size == null || page < 1 || size < 1)
            {
                _logger.LogInformation("Bad arguments");
                return BadRequest();
            }

            var responseModel = await _userService.FindAllAsync(page.Value, size.Value);

            if (responseModel == null)
            {
                _logger.LogError("ResponseModel are null");
                return NotFound();
            }
            return Ok(responseModel);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = _userService.FindByAsync(us => us.LoginName.Equals(name));
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("", Name = "AddUser")]
        public async Task<IActionResult> AddUser([FromBody]UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.FindByAsync((user) => user.LoginName.Equals(userDTO.LoginName));
            if (user != null)
            {
                return Conflict();
            }
            await _userService.AddAsync(_mapper.Map<User>(userDTO));
            return CreatedAtAction(nameof(GetUserByName), new { loginname = userDTO.LoginName }, userDTO);
        }
        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var user = await _userService.FindByAsync((user) => user.LoginName.Equals(name));
        
            if (user == null)
            {
                return NotFound();
            }
        
            await _userService.RemoveAsync(user);
            return NoContent();
        }
    }
}