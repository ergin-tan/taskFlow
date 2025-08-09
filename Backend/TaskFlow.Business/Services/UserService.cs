using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;
using BCrypt.Net;

namespace TaskFlow.Business.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // _unitOfWork = unitOfWork; // base class
        }

        public async Task<User?> GetUserByEmployeeIDAsync(string employeeID)
        {
            return (await _unitOfWork.GetRepository<User>().FindAsync(u => u.EmployeeID == employeeID)).FirstOrDefault();
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await base.GetAllAsync(u => u.Office);
        }

        public override async Task AddAsync(User entity)
        {
            if (!string.IsNullOrEmpty(entity.PasswordHash))
            {
                entity.PasswordHash = HashPassword(entity.PasswordHash);
            }
            await base.AddAsync(entity);
        }

        public override async Task Update(User entity)
        {
            if (!string.IsNullOrEmpty(entity.PasswordHash))
            {
                entity.PasswordHash = HashPassword(entity.PasswordHash);
            }
            await base.Update(entity);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
