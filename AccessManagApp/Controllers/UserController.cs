using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestApp.Interfaces;
using System.Linq;
using System;

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
        [Route("")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {         
            var users = await _userService.FindAllAsync();
            var dtos = _mapper.Map<List<UserDTO>>(users.ToList());
            return Ok(dtos);
        }


        [HttpGet]
        [Route("pagination", Name = "GetUsersWithPagination")]
        public IActionResult GetUsers(int pageNumber, int pageSize)
        {
            if (pageNumber < 1 && pageSize < 1)
            {
                _logger.LogInformation("Bad arguments");
                return BadRequest();
            }
            var responseModel = _userService.FindAllAsync(pageNumber, pageSize);
            if (responseModel == null)
            {
                _logger.LogError("ResponseModel are null");
                return NotFound();
            }
            return Ok(responseModel.Result);
        }

        [HttpGet]
        [Route("{loginname}")]
        public async Task<IActionResult> GetUserByName(string loginName)
        {
            var user = _userService.FindByAsync(us => us.LoginName.Equals(loginName));
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
            if (user == null)
            {
                return Conflict();
            }
            await _userService.AddAsync(_mapper.Map<User>(userDTO));
            return CreatedAtAction(nameof(GetUserByName), new { loginname = userDTO.LoginName }, userDTO);
        }
        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> Delete(string loginName)
        {
            var user = await _userService.FindByAsync((user) => user.LoginName.Equals(loginName));
        
            if (user == null)
            {
                return NotFound();
            }
        
            await _userService.RemoveAsync(user);
            return NoContent();
        }
    }
}