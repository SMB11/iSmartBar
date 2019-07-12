using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Common.Core;
using Facade.Repository;
using BusinessEntities.Global;
using Facade.Managers;

namespace Managers.Implementation
{
    public class ImageManager: IImageManager
    {
        private IServiceProvider serviceProvider;

        public ImageManager(IServiceProvider provider)
        {
            serviceProvider = provider;
        }
        
        public async Task<Image> InsertBytesAsync(byte[] image, CancellationToken token = new CancellationToken())
        {
            string path = await serviceProvider.GetService<IAssetRepository>()
                .InsertAsync(new ImageAsset(image), token);
            return await serviceProvider.GetService<IImageRepository>()
                .InsertAsync(new Image() { Path = path }, token);
        }

        
        public async Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken())
        {
            Image image = (await serviceProvider.GetService<IImageRepository>()
                .FindAsync(i => i.Path == path)).First();
            path = await serviceProvider.GetService<IAssetRepository>()
                .UpdateAsync(new ImageAsset(bytes, path ), token);
            return image;
        }
        
        public async Task RemoveAsync(Image image, CancellationToken token = default(CancellationToken))
        {
            Image original = await serviceProvider.GetService<IImageRepository>().FindByIDAsync(image.ID);
            int id = await serviceProvider.GetService<IImageRepository>().RemoveAsync(original, token);
            if(id != 0)
                serviceProvider.GetService<IAssetRepository>().Remove(original.Path);
        }
    }
}
