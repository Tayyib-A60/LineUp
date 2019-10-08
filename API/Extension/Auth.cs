// // using System;
// // using System.IdentityModel.Tokens.Jwt;
// // using System.Security.Claims;
// // using System.Text;
// // using Microsoft.IdentityModel.Tokens;

// // namespace API.Extension
// // {
// //     public class Auth
// //     {
// //         public User Authenticate (string email, string password) {
// //             if (string.IsNullOrEmpty (email) || string.IsNullOrEmpty (password))
// //                 return null;
// //             var user = _context.Users.SingleOrDefault (u => u.UserEmail == email);
// //             if (user == null)
// //                 return null;
// //             if (!VerifyPasswordHash (password, user.PasswordHash, user.PasswordSalt))
// //                 return null;
// //             return user;
// //         }
// //         public async Task<IEnumerable<User>> GetUsers () {
// //             return await _context.Users.ToListAsync ();
// //         }
// //         public async Task<User> GetUser (int id) {
// //             return await _context.Users
// //                 .Where (u => u.UserId == id)
// //                 .SingleOrDefaultAsync ();
// //         }
// //         public async Task<User> GetUser (string email) {
// //             return await _context.Users
// //                 .Where (u => u.UserEmail == email)
// //                 .SingleOrDefaultAsync ();
// //         }
// //         public async Task<bool> UserExists (User user) {
// //             if (await _context.Users.AnyAsync (x => x.UserEmail == user.UserEmail))
// //                 return true;

// //             return false;
// //         }
// //         public async Task<User> CreateUser (User user, string password) {
// //             if (user == null)
// //                 throw new NullReferenceException ("User cannot be null");
// //             if (string.IsNullOrWhiteSpace (password))
// //                 throw new ArgumentNullException ("Password is Required");
// //             byte[] passwordHash, passwordSalt;
// //             CreatePasswordHash (password, out passwordHash, out passwordSalt);
// //             user.PasswordHash = passwordHash;
// //             user.PasswordSalt = passwordSalt;
// //             // if (user.UserEmail.Contains ("adesokan")) {
// //             //     user.UserRole = Role.Admin;
// //             // } else {
// //             //     user.UserRole = Role.Customer;
// //             // }

// //             _context.Users.Add (user);
// //             await _context.SaveChangesAsync ();

// //             return user;
// //         }
// //         public async Task<bool> ForgotPassword (User user) {
// //             if (await _context.Users.AnyAsync (x => x.UserEmail == user.UserEmail))
// //                 return true;
// //             return false;
// //         }
// //         public void UpdateUser (string newPassword, User userToUpdate) {
// //             if (!string.IsNullOrWhiteSpace (newPassword)) {
// //                 byte[] passwordHash, passwordSalt;
// //                 CreatePasswordHash (newPassword, out passwordHash, out passwordSalt);
// //                 userToUpdate.PasswordHash = passwordHash;
// //                 userToUpdate.PasswordSalt = passwordSalt;
// //                 userToUpdate.LastPasswordChange = DateTime.Now;
// //             }
// //             _context.Users.Update (userToUpdate);
// //             // _context.SaveChangesAsync ();
// //         }
// //         public void DeleteUser (User user) {
// //             _context.Remove (user);
// //         }
// //         public string CreateToken (User user) {
// //             var tokenHandler = new JwtSecurityTokenHandler ();
// //             var key = Encoding.ASCII.GetBytes (_appSettings.Secret);
// //             var sub = new ClaimsIdentity ();
// //             var tokenDescriptor = new SecurityTokenDescriptor {
// //                 Subject = new ClaimsIdentity (new Claim[] {
// //                 new Claim (ClaimTypes.NameIdentifier, user.UserEmail),
// //                 new Claim (ClaimTypes.Name, user.UserName),
// //                 new Claim (ClaimTypes.Role, user.UserRole.ToString ()),
// //                 new Claim (ClaimTypes.GroupSid, user.UserId.ToString())
// //                 }),
// //                 // Expires = DateTime.UtcNow.AddDays(7),
// //                 Expires = DateTime.UtcNow.AddMinutes(180),
// //                 SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
// //             };
// //             var token = tokenHandler.CreateToken (tokenDescriptor);
// //             var tokenString = tokenHandler.WriteToken (token);
// //             return tokenString;
// //         }
// //         private static void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
// //             if (password == null) throw new ArgumentNullException ("password");
// //             if (string.IsNullOrWhiteSpace (password)) throw new ArgumentException ("value cannot be empty or whitespace, on string is allowed ", "password");

// //             using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
// //                 passwordSalt = hmac.Key;
// //                 passwordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
// //             }
// //         }
// //         private static bool VerifyPasswordHash (string password, byte[] storedHash, byte[] storedSalt) {
// //             if (password == null) throw new ArgumentNullException ("password");
// //             if (string.IsNullOrWhiteSpace (password)) throw new ArgumentException ("value cannot be empty or whitespace, only string is allowed ", "password");
// //             if (storedHash.Length != 64) throw new ArgumentException ("Invalid length of password hash (64 bytes expected).", "passwordHash");
// //             if (storedSalt.Length != 128) throw new ArgumentException ("Invalid length of password salt (128 bytes expected).", "passwordHash");

// //             using (var hmac = new System.Security.Cryptography.HMACSHA512 (storedSalt)) {
// //                 var computedHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
// //                 for (int i = 0; i < computedHash.Length; i++) {
// //                     if (computedHash[i] != storedHash[i]) return false;
// //                 }
// //             }
// //             return true;
// //         }
// //     }
// // }
// [AllowAnonymous]
//         [HttpPost ("authenticate")]
//         public IActionResult Authenticate ([FromBody] UserDTO userResource) {
//             var user = _repository.Authenticate (userResource.UserEmail, userResource.Password);

//             if (user == null)
//                 return Unauthorized ();
//             var token = _repository.CreateToken(user);
//             return Ok (new {
//                     Id = user.UserId,
//                     Name = user.UserName,
//                     Email = user.UserEmail,
//                     Token = token,
//                     Roles = user.UserRole.ToString ()
//             });
//         }

//         [AllowAnonymous]
//         [HttpPost("forgotPassword")]
//         public async Task<IActionResult> ForgotPassword ([FromBody] UserDTO userDTO) {
//             // if (!await _repository.ForgotPassword (email.ToString()))
//             //     return BadRequest ("User does not exist");
//             var user = _mapper.Map<User>(userDTO);
//             var token = _repository.CreateToken (user);
//             // var x  = _configuration.
//             var apiKey = _configuration.GetSection("SkineroMotorsSendGridApiKey").Value;
//             var sendGridclient = new SendGridClient (apiKey);
//             var from = new EmailAddress ("info@skineromotors.com", "SkineroMotors");
//             var subject = "Skinero Motors Contact us";
//             var to = new EmailAddress (user.UserEmail, user.UserName);
//             // var buttonName = "Reset Password";
//             var plainTextContent1 = $"<strong>http://localhost:4200/user/resetPassword?token={token}</strong>";
//             var htmlContent = $"<div style='background-color: #f44336; text-align: center;color:rgb(35, 41, 41); margin: 0 auto; padding: 50px; width: 75%'><div style='background-color: #ebebeb; height: 60% !important;'><h2 style='text-align: center; padding-top: 15px;'><strong>Welcome to Skinero Motors</strong></h2><h4>Hello {user.UserName},</h4><p>A transaction was initiated to change your password, Click the button below to reset your password</p> <br> <a class='resetBtn' href='http://localhost:4200/user/resetpassword?token={token}' style='background-color: #f44336; color: #0c0b0b; padding: 5px; border-radius: 3px;'>Reset Password</a><br><p>&nbsp;<p></div></div>";
//             var msg = MailHelper.CreateSingleEmail (from, to, subject, plainTextContent1, htmlContent);
//             var response = await sendGridclient.SendEmailAsync (msg);

//             return Ok ();
//         }

//         [AllowAnonymous]
//         [HttpPost ("register")]
//         public async Task<IActionResult> Register ([FromBody] UserDTO userDTO) {
//             userDTO.LastPasswordChange = DateTime.Now;
//             var user = _mapper.Map<User> (userDTO);
//             if (await _repository.UserExists (user))
//                 return BadRequest ("User already exists");
//             try {
//                 await _repository.CreateUser (user, userDTO.Password);
//                 return Ok ($"User with email {user.UserEmail} Created");
//             } catch (Exception ex) {
//                 return BadRequest (ex.Message);
//             }
//         }