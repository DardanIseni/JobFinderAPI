using JwtAuthAspNet7WebAPI.Controllers;
using JwtAuthAspNet7WebAPI.Dtos;
using JwtAuthAspNet7WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class UnitTest1
{
        private AuthController _authController;
        private Mock<IAuthService> _authServiceMock;

        public UnitTest1()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task RegisterDemander_ValidRegisterDto_ReturnsOkResult()
        {
            var registerDto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "password123"
            };
            var expectedRegisterResult = new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "Registration successful",
                User = null
            };
            _authServiceMock.Setup(service => service.RegisterDemanderAsync(registerDto)).ReturnsAsync(expectedRegisterResult);

            var result = await _authController.RegisterDemander(registerDto);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedRegisterResult, okResult.Value);
        }

        [Fact]
        public async Task RegisterSeeker_ValidRegisterDto_ReturnsOkResult()
        {
            var registerDto = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = "password123"
            };
            var expectedRegisterResult = new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "Registration successful",
                User = null
            };
            _authServiceMock.Setup(service => service.RegisterSeekerAsync(registerDto)).ReturnsAsync(expectedRegisterResult);

            var result = await _authController.RegisterSeeker(registerDto);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedRegisterResult, okResult.Value);
        }

        [Fact]
        public async Task Login_ValidLoginDto_ReturnsOkResult()
        {
            var loginDto = new LoginDto
            {
                UserName = "string",
                Password = "dardaniseni123"
            };            
            var expectedLoginResult = new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "Success"
            };
            _authServiceMock.Setup(service => service.LoginAsync(loginDto)).ReturnsAsync(expectedLoginResult);

            var result = await _authController.Login(loginDto);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedLoginResult, okResult.Value);
        }
}