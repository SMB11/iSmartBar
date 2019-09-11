using BusinessEntities.Statistics;
using SharedEntities.DTO.Statistics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IVisitManager
    {
        Task<List<VisitDTO>> GetAllAsync();

        Task<int> InsertAsync(VisitDTO visit);

    }
}
