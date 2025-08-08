using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;

namespace TaskFlow.Business.Services
{
    public class TaskAssignmentService : GenericService<TaskAssignment>, ITaskAssignmentService
    {
        public TaskAssignmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
