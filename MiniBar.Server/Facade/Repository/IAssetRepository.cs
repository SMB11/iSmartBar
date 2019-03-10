using BusinessEntities.Global;
using System.Threading;
using System.Threading.Tasks;

namespace Facade.Repository
{
    public interface IAssetRepository
    {
        string Insert(Asset asset);
        Task<string> InsertAsync(Asset asset, CancellationToken token = new CancellationToken());
        string Update(Asset asset);
        Task<string> UpdateAsync(Asset asset, CancellationToken token = new CancellationToken());
        void Remove(string path);
    }
}
