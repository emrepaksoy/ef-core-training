using AutoMapper;
using AutoMapperDemo.API.DTO;
using AutoMapperDemo.API.Entities;
using AutoMapperDemo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperDemo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFoo _foo;
        private readonly IBar _bar;
        public UsersController(IUserRepository userRepository, IMapper mapper,IFoo foo,IBar bar)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _foo = foo;
            _bar = bar;
        }

        [HttpGet]
        public ActionResult<List<UserReadDTO>> Get()
        {
            var users = _userRepository.GetAllUser();
            var usersReadDto = _mapper.Map<List<UserReadDTO>>(users);

            return Ok(usersReadDto);

        }

        [HttpGet("{id}")]

        public ActionResult<List<UserReadDTO>> Get(int id)
        {
            //return Ok(_userRepository.GetUserById(id));

            var user = _userRepository.GetUserById(id);
            if (user == null)
                return BadRequest("USer not found");

            //var userReadDto = _mapper.Map<List<UserReadDTO>>(user);
            var userReadDto = new UserReadDTO()
            {
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Age = HelperFunctions.HelperFunctions.GetCurrentAge(user.DateOfBirth)
            };
            return Ok(userReadDto);
        }


        [HttpPost]
        public ActionResult<UserReadDTO> Add(UserCreateDTO user)
        {
            var userToCreate = _mapper.Map<User>(user);
            var createdUser = _userRepository.CreateUser(userToCreate);

            var userReadDto = _mapper.Map<UserReadDTO>(createdUser);
            return Ok(_mapper.Map<UserReadDTO>(userReadDto));
        }

        [HttpGet("scl")]

        public ActionResult<string> TestScl()
        {
            var g1 = _bar.Getvalue2();
            var g2 = _foo.GetValue();
            //return Ok(_userRepository.GetUserById(id));
            
            return Ok($"Foo Value {g2} ----- Bar Value {g1}");
        }

    }

}
