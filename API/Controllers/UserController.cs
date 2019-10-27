using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.DTOs;
using API.Core;
using API.Core.Models;
using API.Extension;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Primitives;

namespace API.Controllers {
    [Route ("api/lineUp/user")]
    [ApiController]
    public class UserController : ControllerBase {
        private IUserRepository _userRepository { get; }
        private IConfiguration _configuration { get; }
        private IMapper _mapper { get; }
        public UserController (IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] UserDTO userDTO) {
            var user = _userRepository.Authenticate (userDTO.Email, userDTO.Password);
            if (user == null)
                return Unauthorized ();
            var tokenString = _userRepository.CreateToken(user);
            return Ok (new {
                Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Token = tokenString,
                    Roles = user.Role.ToString()
            });
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword ([FromBody] UserDTO userResource) {
            var user = _mapper.Map<User>(userResource);
            var token = _userRepository.CreateToken (user);
            StringValues origin;
            Request.Headers.TryGetValue("Origin", out origin);
            var apiKey = _configuration.GetSection ("234SpacesSendGridApiKey").Value;
            var sendGridclient = new SendGridClient (apiKey);
            var from = new EmailAddress ("info@234spaces.com", "234Spaces");
            var subject = "Reset your password";
            var to = new EmailAddress (user.Email, user.Name);
            var plainTextContent1 = $"<strong>http://localhost:4200/resetPassword?token={token}</strong>";
            var htmlContent = $"{origin}/reset-password?token={token}";
            var msg = MailHelper.CreateSingleEmail (from, to, subject, plainTextContent1, htmlContent);
            var response = await sendGridclient.SendEmailAsync (msg);
            return Ok ();
        }

        [AllowAnonymous]
        [HttpPost ("createCustomer")]
        public async Task<IActionResult> CreateClient ([FromBody] UserToSignUpDTO userResource)
        {
            var user = _mapper.Map<User> (userResource);
            user.Role = Role.Client;
            user.EmailVerified = false;
            if (await _userRepository.UserExists (user))
                return BadRequest ("User already exists");
            try {
                var userCreated = await _userRepository.CreateUser (user, userResource.Password);
                if(userCreated.Email == user.Email) {
                    StringValues origin;
                    var token = _userRepository.CreateToken(user);
                    Request.Headers.TryGetValue("Origin", out origin);
                    Message message = new Message();
                    message.Subject = "Account Confirmation";
                    message.FromEmail = "noreply@234spaces.com";
                    message.FromName = "234Spaces Admin";
                    message.ToName = user.Name;
                    message.ToEmail = user.Email;
                    message.PlainContent = null;
                    message.HtmlContent = MailString(origin, token);
                    _userRepository.EmailSender(message);
                }
                return Ok ();
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }
        
        [AllowAnonymous]
        [HttpPost ("createMerchant")]
        public async Task<IActionResult> CreateMerchant ([FromBody] UserToSignUpDTO userResource)
        {
            var user = _mapper.Map<User> (userResource);
            user.Role = Role.Merchant;
            user.EmailVerified = false;
            if (await _userRepository.UserExists (user))
                return BadRequest ("User already exists");
            try {
                var userCreated = await _userRepository.CreateUser (user, userResource.Password);
                if(userCreated.Email == user.Email) {
                    StringValues origin;
                    var token = _userRepository.CreateToken(user);
                    Request.Headers.TryGetValue("Origin", out origin);
                    Message message = new Message();
                    message.Subject = "Account Confirmation";
                    message.FromEmail = "noreply@234spaces.com";
                    message.FromName = "234Spaces Admin";
                    message.ToName = user.Name;
                    message.ToEmail = user.Email;
                    message.PlainContent = null;
                    message.HtmlContent = MailString(origin, token);
                    _userRepository.EmailSender(message);
                }
                return Ok ();
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost ("createSuperAdmin")]
        public async Task<IActionResult> CreateSuperAdmin ([FromBody] UserToSignUpDTO userResource)
        {
            var user = _mapper.Map<User> (userResource);
            user.Role = Role.AnySpaces;
            // user.EmailVerified = false;
            if (await _userRepository.UserExists (user))
                return BadRequest ("User already exists");
            try {
                var userCreated = await _userRepository.CreateUser (user, userResource.Password);
                if(userCreated.Email == user.Email) {
                    StringValues origin;
                    var token = _userRepository.CreateToken(user);
                    Request.Headers.TryGetValue("Origin", out origin);
                    Message message = new Message();
                    message.Subject = "Account Confirmation";
                    message.FromEmail = "noreply@234spaces.com";
                    message.FromName = "234Spaces Admin";
                    message.ToName = user.Name;
                    message.ToEmail = user.Email;
                    message.PlainContent = null;
                    message.HtmlContent = MailString(origin, token);
                    _userRepository.EmailSender(message);
                }
                return Ok ();
            } catch (Exception ex) {
                return BadRequest (ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("resendMailForEmailVerification")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] UserToSignUpDTO userToSignUpDTO)
        {
            var userToVerifyEmail = _mapper.Map<User>(userToSignUpDTO);
            if(await _userRepository.UserExists(userToVerifyEmail)) {
                var user = await _userRepository.GetUser (userToVerifyEmail.Email);
            if (user == null)
                return NotFound("Specified user doesn't exist");
                StringValues origin;
                var token = _userRepository.CreateToken(user);
                Request.Headers.TryGetValue("Referer", out origin);
                Message message = new Message();
                message.Subject = "Account Confirmation";
                message.FromEmail = "noreply@234spaces.com";
                message.FromName = "234Spaces Admin";
                message.ToName = user.Name;
                message.ToEmail = user.Email;
                message.PlainContent = null;
                message.HtmlContent = MailString("origin", token);
                _userRepository.EmailSender(message);
                return Ok();
            }
            return BadRequest("An unexpected error occured");

        }
        [AllowAnonymous]
        [HttpPost("verifyUserEmail")]
        public async Task<IActionResult> VerifyUser([FromBody] UserToSignUpDTO userToSignUpDTO)
        {
            var userToVerifyEmail = _mapper.Map<User>(userToSignUpDTO);
            if(userToVerifyEmail == null)
                return BadRequest("User cannot be null");
            if(await _userRepository.UserExists(userToVerifyEmail) == false)
                return BadRequest("User does not exist");
            userToVerifyEmail.EmailVerified = true;
            _userRepository.VerifyUserEmail(userToVerifyEmail);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll () {
            var users = await _userRepository.GetUsers ();
            var usersResource = _mapper.Map<IEnumerable<UserDTO>> (users);
            return Ok (usersResource);
        }

        [HttpGet ("byId/{id}")]
        public async Task<IActionResult> GetUser (int id)
        {
            var user = await _userRepository.GetUser (id);
            var userDTO = _mapper.Map<UserDTO> (user);
            return Ok (userDTO);
        }

        [HttpGet ("{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser (string email) {
            var user = await _userRepository.GetUser (email);
            if (user == null)
                return NotFound("Specified user doesn't exist");
            var userDTO = _mapper.Map<UserDTO> (user);
            return Ok (userDTO);
        }

        [HttpPut ("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser (int id, [FromBody] UserDTO userResource) {
            if (userResource == null)
                return BadRequest("User cannot be null");
            var userToUpdate = await _userRepository.GetUser(id);
            if (userToUpdate == null)
                return NotFound("User does not exist");
             _mapper.Map<UserDTO, User> (userResource, userToUpdate);
             _userRepository.UpdateUser(userResource.Password, userToUpdate);
             return Ok();
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (int id) {
            var user = await _userRepository.GetUser (id);
            _userRepository.DeleteUser (user);
            return Ok (id);
        }
        private string MailString(string origin, string token)
        {
            return $"<br/><br/><div style='background-color: #FFEFD5; height: 60% !important;'><h2 style='text-align: center; padding-top: 15px;'><strong>Welcome to 234Spaces</strong></h2><p>To verify your email, please click the button or link below to verify your email</p> <br> <a class='resetBtn' href='{origin.ToString()}/verifyEmail?token={token}' style='background-color: #00FF7F; color: #0c0b0b; padding: 6px; border-radius: 3px; text-decoration: none;'>Verify Email</a><br><p>&nbsp;<p><p>{origin.ToString()}/verifyEmail?token={token}</p></div>";
        }
    }
}