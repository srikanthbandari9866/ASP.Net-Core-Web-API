using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepo userRepo, IMapper mapper) 
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userDomain = await _userRepo.GetAllAsync();
            //return Ok(userDomain);
            return Ok(_mapper.Map<List<UserDto>>(userDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRegisterDto obj)
        {
            var userDomain = _mapper.Map<User>(obj);

            userDomain = await _userRepo.RegisterUserAsync(userDomain);

            return Ok(_mapper.Map<UserDto>(userDomain));

        }
        [HttpPost]
        [Route("encrypt")]
        public async Task<IActionResult> CreateEncrypt(UserRegisterDto obj)
        {
            var userDomain = _mapper.Map<User>(obj);

            userDomain = await _userRepo.RegisterUserEncryptAsync(userDomain);

            return Ok(_mapper.Map<UserDto>(userDomain));

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var userDomain = _mapper.Map<User>(userLoginDto);
           
            userDomain = await _userRepo.LoginAsync(userDomain.Email, userDomain.Password);

            if(userDomain == null)
            {
                return Unauthorized();
            }

            var token = _userRepo.CreateTokenAsync(userDomain);

            return Ok(token);

            //var response = new LoginResponseDto()
            //{
            //    Token = token,
            //    UserId = userDomain.UserId,
            //    UserName = userDomain.UserName,
            //    Email = userDomain.Email,
            //    Role = userDomain.Role,
            //};
            //return Ok(response);
        }

        [HttpPost]
        [Route("loginEncrypt")]
        public async Task<IActionResult> Logins([FromBody] UserLoginDto userLoginDto)
        {
            var userDomain = _mapper.Map<User>(userLoginDto);
            userDomain = await _userRepo.LoginsAsync(userDomain.Email, userDomain.Password, userDomain);

            if (userDomain == null)
            {
                return Unauthorized();
            }

            var token = _userRepo.CreateTokenAsync(userDomain);

            return Ok(token);

            //var response = new LoginResponseDto()
            //{
            //    Token = token,
            //    UserId = userDomain.UserId,
            //    UserName = userDomain.UserName,
            //    Email = userDomain.Email,
            //    Role = userDomain.Role,
            //};
            //return Ok(response);
        }
    }

}
