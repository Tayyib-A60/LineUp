using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core
{
    public interface IUserRepository
    {
        User Authenticate(string email, string password);
        void EmailSender(Message message);
        void DeleteUser(User user);
        Task<bool> ForgotPassword(User user);
        Task<bool> UserExists(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUser(string email);
        Task<User> CreateUser(User user, string password);
        void UpdateUser (string newPassword, User userToUpdate);
        void UpdateUserStatus(User userToVerify);
        void VerifyAsMerchant (User user);
        string CreateToken(User user);
    }
}