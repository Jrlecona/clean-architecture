using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.UseCases;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly IUserRepository _userRepository;

        public UserController(CreateUserUseCase createUserUseCase, IUserRepository userRepository)
        {
            _createUserUseCase = createUserUseCase;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User _user)
        {
            try
            {
                var user = await _createUserUseCase.Execute(
                    _user.Name.ToString(),
                    _user.Email.ToString()
                );
                return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }
    }
}
