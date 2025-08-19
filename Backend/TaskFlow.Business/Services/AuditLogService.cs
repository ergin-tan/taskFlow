
using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class AuditLogService : GenericService<AuditLog>, IAuditLogService
    {
        public AuditLogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
