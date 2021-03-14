using AccessManagApp.Controllers;
using AutoMapper;
using Castle.Core.Logging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Interfaces;
using Xunit;

namespace AccessManagementAppTests
{
    public class UserControllerTests
    {
        private Mock<ILogger<User>> _mockLogger;
        private Mock<IUserService> _mockUserService;
        private Mock<IMapper> _mockMapper;
        private UsersController _userController;

        [SetUp]
        public void SetUp()
        {
            _mockUserService= new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<User>>();
            _mockMapper = new Mock<IMapper>();
            _userController = new UsersController(_mockLogger.Object, _mockUserService.Object, _mockMapper.Object);
        }

        [Test]
        [Fact]
        public async Task GetUsers_ReturnsValidUsers()
        {
             _mockMapper.Setup(m => m.Map<List<UserDTO>>(It.IsAny<List<User>>())).Returns(GetMockedUserDTOs());

            ActionResult<IEnumerable<UserDTO>> actionResult = await _userController.Get();
            var res = actionResult.Result as OkObjectResult;
            var userDTOs = (List<UserDTO>)res.Value;
            Assert.IsNotNull(userDTOs);
            Assert.AreEqual(2, userDTOs.Count());
            Assert.AreEqual("User1", userDTOs[0].LoginName);
            Assert.AreEqual("User2", userDTOs[1].LoginName);
        }

        private List<UserDTO> GetMockedUserDTOs()
        {
            return new List<UserDTO>()
                       {
                            new UserDTO()
                            {
                               LoginName = "User1"
                            },
                            new UserDTO()
                            {
                               LoginName = "User2"
                            }
                       };
        }

        private List<User> GetMockedUsers()
        {
            return new List<User>()
                       {
                            new User()
                            {
                               LoginName = "User1"
                            },
                            new User()
                            {
                               LoginName = "User2"
                            }
                       };
        }

        [Test]
        [Fact]
        public async Task Delete_ExistingUser()
        {
             Func<User, bool> dunc = null;
            _mockUserService.Setup(s => s.FindByAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User() { LoginName = "User1"});

             var actionResult = await _userController.Delete("User1");
             var res = actionResult as NoContentResult;
             Assert.IsNotNull(res);
        }

        [Test]
        [Fact]
        public async Task Delete_NotExistingUserName_ReturnsNotFound()
        {
            var actionResult = await _userController.Delete("User1");
            var res = actionResult as NotFoundResult;
            Assert.IsNotNull(res);
        }

        [Test]
        [Fact]
        public async Task AddUser_InvalidModelState_ReturnsBadRequest()
        {
            UserDTO userDTO = new UserDTO()
            {
                LoginName = "Test1"
            };

            _userController.ModelState.AddModelError("Email", "Required");
            var actionResult = await _userController.AddUser(userDTO);
            var res = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(res);
        }

        [Test]
        [Fact]
        public async Task AddUser_ExistingUser_ReturnsConflict()
        {
            UserDTO userDTO = new UserDTO()
            {
                LoginName = "Test1"
            };
            _mockUserService.Setup(s => s.FindByAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User());
            var actionResult = await _userController.AddUser(userDTO);
            var res = actionResult as ConflictResult;

            Assert.IsNotNull(res);
        }

        [Test]
        [Fact]
        public async Task AddUser_User_ReturnsValidRoute()
        {
            UserDTO userDTO = new UserDTO()
            {
                LoginName = "User1"
            };

            var actionResult = await _userController.AddUser(userDTO);
            var res = actionResult as CreatedAtActionResult;

            Assert.IsNotNull(res);
            Assert.AreEqual("GetUserByName", res.ActionName);
            Assert.AreEqual("User1", res.RouteValues["loginname"]);
        }

    }
}
