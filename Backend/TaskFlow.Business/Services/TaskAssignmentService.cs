using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Business.Services
{
    public class TaskAssignmentService : GenericService<TaskAssignment>, ITaskAssignmentService
    {
        public TaskAssignmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<TaskAssignment>> GetAllAsync()
        {
            return await base.GetAllAsync(ta => ta.WorkTask, ta => ta.AssignedToUser);
        }
    }
}
