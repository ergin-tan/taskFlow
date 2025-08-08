using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class TaskHistoryService : GenericService<TaskHistory>, ITaskHistoryService
    {
        public TaskHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
