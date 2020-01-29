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
                return Unauthorized();
            if(!user.EmailVerified)
                return Unauthorized();
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
                    message.HtmlContent = FormattedEmailBody("Account Confirmation", $"Please confirm that {user.Email} is your email address by clicking on the button below", $"{origin.ToString()}/confirm-email?token={token}", "Confirm Account", true);
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
                    message.HtmlContent = FormattedEmailBody("Account Confirmation", $"Please confirm that {user.Email} is your email address by clicking on the button below", $"{origin.ToString()}/confirm-email?token={token}", "Confirm Account", true);
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
                    message.HtmlContent = FormattedEmailBody("Account Confirmation", $"Please confirm that {user.Email} is your email address by clicking on the button below", $"{origin.ToString()}/confirm-email?token={token}", "Confirm Account", true);
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
                message.HtmlContent = FormattedEmailBody("Account Confirmation", $"Please confirm that {user.Email} is your email address by clicking on the button below", $"{origin.ToString()}/confirm-email?token={token}", "Confirm Account", true);
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
            _userRepository.UpdateUserStatus(userToVerifyEmail);
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

        [HttpPut("verifyAccount")]
        public async Task<IActionResult> VerifyUserAccount([FromBody] UserDTO userResource) {
            if (userResource == null)
                return BadRequest("User cannot be null");
            var userToUpdate = await _userRepository.GetUser(userResource.Id);
            if(userToUpdate.EmailVerified || userToUpdate.Enabled)
                return Ok();
            if (userToUpdate == null)
                return NotFound("User does not exist");
            //  _mapper.Map<UserDTO, User> (userResource, userToUpdate);
             _userRepository.UpdateUserStatus(userToUpdate);
             return Ok();
        }
        [HttpPut("verifyAsMerchant")]
        public async Task<IActionResult> VerifyAsMerchant([FromBody] UserDTO userResource) {
            if (userResource == null)
                return BadRequest("User cannot be null");
            var userToUpdate = await _userRepository.GetUser(userResource.Id);
            if (userToUpdate == null)
                return NotFound("User does not exist");
            //  _mapper.Map<UserDTO, User> (userResource, userToUpdate);
             _userRepository.VerifyAsMerchant(userToUpdate);
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
            return $"<br/><br/><div style='background-color: #FFEFD5; height: 60% !important;'><h2 style='text-align: center; padding-top: 15px;'><strong>Welcome to 234Spaces</strong></h2><p>To verify your email, please click the button or link below to verify your email</p> <br> <a class='resetBtn' href='{origin.ToString()}/confirm-email?token={token}' style='background-color: #00FF7F; color: #0c0b0b; padding: 6px; border-radius: 3px; text-decoration: none;'>Verify Email</a><br><p>&nbsp;<p><p>{origin.ToString()}/confirm-email?token={token}</p></div>";
        }
        private string FormattedEmailBody(string heading, string body, string buttonLink, string buttonName, bool hasButton)
        {
            string first = "<!DOCTYPE html> <html lang='en'> <head><meta charset='UTF-8'><meta name='viewport' content='width=device-width, initial-scale=1.0'><meta http-equiv='X-UA-Compatible' content='ie=edge'><title>234Spaces</title><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css'><link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css'></head><body><style>*,*::before { padding: 0; box-sizing: inherit;}html {box-sizing: border-box;}@media only screen and (max-width: 37.5em) { html {font-size: 62.5%; }}@media only screen and (max-width: 25em) {html { font-size: 50%;}}body {font-family: 'Nunito', sans-serif;color: #6D5D4B;font-weight: 300; outline: none; line-height: 1.6;   max-width: 100%;}.content {height: 100vh;}@media only screen and (max-width: 31.25em) {.header {padding: .2rem !important;}.header-link {padding: 0 1rem 0 1rem !important;}.booking {text-align: center !important;}}.header-link, .header-link:hover {color: #EAEAEA;font-size: 1.5rem;text-decoration: none;}.booking {font-size: 1rem;text-transform: uppercase;}.sub-head {padding: 1.5rem 0 1.5rem 0;border-bottom: 1px solid #aaa;margin-bottom: .5rem;}.logo{height: 10%;width: 7.5%;}.btn-2 {padding: .375rem .75rem !important;}.footer {line-height: .5; margin-bottom: 1.5rem;font-size: .8rem;}@media only screen and (max-width: 25em) { .footer { font-size: 1rem; }.footer-icon {height: 2rem !important;width: 2rem !important;padding: 2.22px !important;font-size: 1.3rem !important;}.footer-icon {height: 2.5rem;width: 2.5rem;padding: 6px;font-size: 1.465rem;vertical-align: middle;text-align: center;border-radius: 50%;background-color: #2B2C2E;color: #EBEBEB;}</style><div class='content'><div class='header'><a href='#' class='px-5 header-link'><img class='logo' src='https://res.cloudinary.com/dro0fy3gz/image/upload/v1576489895/Logo/234Spaces_logo_PNG_2_qxc8rn.png'></a></div><div class='px-3'><h3 class='pt-1 pb-1 text-center'>" + heading + "</h3><div class='sub-head'><div class='d-flex justify-content-center align-item-center'><div class='lead'><p>" + body + "</p></div></div></div>";
            string second = hasButton? "<div class='sub-head'><div class='d-flex justify-content-center align-item-center'><div><a href='" + buttonLink + "' class='btn btn-primary btn-lg'>"+ buttonName +"</a></div></div></div>" : "";
            string third = "</div><div class='jumbotron p-3'><div class='d-flex justify-content-end mb-4 mt-5'><a href=''><i class='fa fa-facebook mx-2 footer-icon'></i></a><a href=''><i class='fa fa-twitter mx-2 footer-icon'></i></a><a href=''><i class='fa fa-linkedin mx-2 footer-icon'></i></a> </div><div class='footer'><p>234Spaces Inc., 2019. All rights reserved.</p><p>229 West 43rd Street 5th Floor New York, NY 10036</p></div></div></div></body></html>";

            return first + second + third;
        }
    }
}