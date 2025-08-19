using TaskFlow.Core.Models;
using TaskFlow.Core.Services;
using TaskFlow.Core.UnitOfWorks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskFlow.Business.Services
{
    public class WorkTaskService : GenericService<WorkTask>, IWorkTaskService
    {
        public WorkTaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<WorkTask>> GetAllAsync()
        {
            return await base.GetAllAsync(wt => wt.AssignedByUser);
        }

        public override async Task Update(WorkTask entity)
        {
            var originalTask = await GetByIdAsNoTrackingAsync(entity.Id);

            if (originalTask != null)
            {
                int changedByUserId = entity.AssignedBy; 
                entity.AssignedBy = originalTask.AssignedBy;
            }

            await base.Update(entity);
        }
    }
}