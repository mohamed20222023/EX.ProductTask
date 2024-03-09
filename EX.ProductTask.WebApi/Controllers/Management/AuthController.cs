//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Application.IBusiness.Management;
//using Application.Dtos.Users;

//namespace Users.API.Controllers.Management
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    [AllowAnonymous]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthBusiness _iAuthRepository;
//        public AuthController(
//            IAuthBusiness IAuthRepository
//            )
//        {
//            _iAuthRepository = IAuthRepository;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register( [FromForm] UserRegisterDto userRegister)
//        {
//            var result = await _iAuthRepository.Register(userRegister);
//            if (result.Status)
//                return NoContent();
//            else
//                return BadRequest(result.Message);
//        }

//        [HttpPost("AddUser")]
//        public async Task<IActionResult> AddUser( [FromForm] UserRegisterDto userRegister)
//        {
//            var result = await _iAuthRepository.AddUser(userRegister);
//            if (result.Status)
//                return NoContent();
//            else
//                return BadRequest(result.Message);
//        }
        
//        //[HttpPost("login")]
//        //public async Task<IActionResult> Login(UserLoginDto userLoginDto)
//        //{
//        //    var result = await _iAuthRepository.Login(userLoginDto);
//        //    if (result.Status)
//        //        return Ok(result.ReturnEntity);
//        //    else if (result.StatusCode == 401)
//        //        return Unauthorized(result.Message);
//        //    else
//        //        return BadRequest(result.Message);
//        //}

//    }
//}
