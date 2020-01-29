using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Core;
using API.Core.Models;
using API.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Persistence
{
    public class UserRepository : IUserRepository
    {
        private LineUpContext _context { get; }
        private AppSettings _appSettings { get; }
        private IConfiguration _configuration { get; }
        public UserRepository (LineUpContext context, IOptions<AppSettings> appSettings, IConfiguration configuration) {
            _context = context;
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }
        public User Authenticate (string email, string password) {
            if (string.IsNullOrEmpty (email) || string.IsNullOrEmpty (password))
                return null;
            var user = _context.Users.SingleOrDefault (u => u.Email == email);
            if (user == null)
                return null;
            if (!VerifyPasswordHash (password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }
        public async Task<IEnumerable<User>> GetUsers () {
            return await _context.Users.ToListAsync ();
        }
        public async Task<User> GetUser (int id) {
            return await _context.Users
                .Where (u => u.Id == id)
                .SingleOrDefaultAsync ();
        }
        public async Task<User> GetUser (string email) {
            return await _context.Users
                .Where (u => u.Email == email)
                .SingleOrDefaultAsync ();
        }
        public async Task<bool> UserExists (User user) {
            if (await _context.Users.AnyAsync (x => x.Email == user.Email))
                return true;

            return false;
        }
        public async Task<User> CreateUser (User user, string password)
        {
            if (user == null)
                throw new NullReferenceException ("User cannot be null");
            if (string.IsNullOrWhiteSpace (password))
                throw new ArgumentNullException ("Password is Required");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash (password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add (user);
            await _context.SaveChangesAsync ();

            return user;
        }
        public void UpdateUserStatus(User userToVerify)
        {
            userToVerify.EmailVerified = true;
            userToVerify.Enabled = true;
            _context.Users.Update(userToVerify);
            _context.SaveChangesAsync();
        }
        public async void VerifyAsMerchant (User user)
        {
            user.VerifiedAsMerchant = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async void EmailSender(Message message)
        {
            var apiKey = _configuration.GetSection("234SpacesSendGridApiKey").Value;
            var sendGridclient = new SendGridClient (apiKey);
            var from = new EmailAddress (message.FromEmail, message.FromName);
            var subject = message.Subject;
            var to = new EmailAddress (message.ToEmail, message.ToName);
            var htmlContent = $"<div style='background-color: #ffffff; margin: 0 auto;  color: rgb(30, 31, 30);'><div  style='background-color: #fcd2d2; padding: 12px; border-top-left-radius: 8px; border-top-right-radius: 8px;'><img src='./logo-1446293_640.png' style='max-height: 36px; max-width: 36px' /></div><div style='background-color: #ffffff; padding: 20px; font-size: 20px'>{message.HtmlContent}</div><div style='background-color: #fcd2d2; padding: 9px; border-bottom-left-radius: 8px;border-bottom-right-radius: 8px;'></div></div>";
            var msg = MailHelper.CreateSingleEmail (from, to, subject, null, message.HtmlContent);
            var response = await sendGridclient.SendEmailAsync (msg);
        }
        public async Task<bool> ForgotPassword (User user) {
            if (await _context.Users.AnyAsync (x => x.Email == user.Email))
                return true;
            return false;
        }
        public void UpdateUser (string newPassword, User userToUpdate) {
            if (!string.IsNullOrWhiteSpace (newPassword)) {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash (newPassword, out passwordHash, out passwordSalt);
                userToUpdate.PasswordHash = passwordHash;
                userToUpdate.PasswordSalt = passwordSalt;
            }
            _context.Users.Update (userToUpdate);
            _context.SaveChangesAsync ();
        }
        public void DeleteUser (User user) {
            _context.Remove (user);
            _context.SaveChangesAsync ();
        }
        public string CreateToken (User user) {
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_appSettings.Secret);
            var sub = new ClaimsIdentity ();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new Claim[] {
                new Claim (ClaimTypes.NameIdentifier, user.Email),
                new Claim (ClaimTypes.Name, user.Name),
                new Claim (ClaimTypes.Role, user.Role.ToString ()),
                new Claim (ClaimTypes.GroupSid, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes (120),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            var tokenString = tokenHandler.WriteToken (token);
            return tokenString;
        }
        private static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            if (password == null) throw new ArgumentNullException ("password");
            if (string.IsNullOrWhiteSpace (password)) throw new ArgumentException ("value cannot be empty or whitespace, on string is allowed ", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }
        private static bool VerifyPasswordHash (string password, byte[] storedHash, byte[] storedSalt) {
            if (password == null) throw new ArgumentNullException ("password");
            if (string.IsNullOrWhiteSpace (password)) throw new ArgumentException ("value cannot be empty or whitespace, only string is allowed ", "password");
            if (storedHash.Length != 64) throw new ArgumentException ("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException ("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512 (storedSalt)) {
                var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < computedHash.Length; i++) {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}