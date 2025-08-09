using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Business.Services
{
    public class TaskHistoryService : GenericService<TaskHistory>, ITaskHistoryService
    {
        public TaskHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<TaskHistory>> GetAllAsync()
        {
            return await base.GetAllAsync(th => th.WorkTask, th => th.ChangedByUser);
        }
    }
}
