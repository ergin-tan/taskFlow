using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class WorkTaskService : GenericService<WorkTask>, IWorkTaskService
    {
        public WorkTaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
