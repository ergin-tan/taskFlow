using TaskFlow.Core.Models;
using TaskFlow.Core.Services;

namespace TaskFlow.Core.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User?> GetUserByEmployeeIDAsync(string employeeID);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
