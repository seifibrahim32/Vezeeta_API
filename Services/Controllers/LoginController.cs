using Domain.Models;
using Domain.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.IRepository;

namespace Services.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IEmailSenderRepository _emailRepository;

        public LoginController(IUserRepository userRepository, IEmailSenderRepository emailRepository)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> SignInWithEmailAndPassword(SignInModel signInModel)
        {
            string jwtToken = await _userRepository.SignIn(signInModel);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                var response = new ResponseLoginModel()
                {
                    StatusCode = 200,
                    Email = signInModel.Email,
                    AccessToken = jwtToken
                };
                return StatusCode(200, response);
            }
            return NotFound("The email isn't registered with the platform. Create One");
        }

        [HttpPost, Route("Register")]
        public async Task<IActionResult> SignUpWithEmailAndPassword(
            [FromBody] SignUpModel signUpModel
         )
        {
            var result = await _userRepository.Insert(signUpModel);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            if (signUpModel.Email != null)
            {
                await _emailRepository.SendEmailAsync(signUpModel);

                var response = new SuccessResponseModel()
                {
                    StatusCode = 200,
                    Email = signUpModel.Email,
                    Message = "Your email is registered successfully.."
                };
                return Ok(response);
            }
            return NotFound();
        }

        [HttpGet, Route("SignInWithGoogle")]
        [AllowAnonymous]
        public IActionResult SignUpWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "login",
                                    new { ReturnUrl = "https//google.com" });

            if (redirectUrl != null)
            {

                var properties = _userRepository.SignInWithGoogle(redirectUrl);
                return new ChallengeResult("Google", properties);

            }

            return NotFound();
        }

        [HttpGet, ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GoogleResponse()
        {
            string data = await _userRepository.SendResponseCallbackFromGoogle();

            if (data != "true")
            {

                return NotFound(data);

            }
            return Ok(data);
        }
    }
}
