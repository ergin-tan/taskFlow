using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class OfficeService : GenericService<Office>, IOfficeService
    {
        public OfficeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
