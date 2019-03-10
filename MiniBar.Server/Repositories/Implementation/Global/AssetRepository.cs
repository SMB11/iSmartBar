﻿using BusinessEntities.Global;
using Facade.Repository;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Implementation.Global
{
    public class AssetRepository : IAssetRepository
    {
        private string GetFullPath(string relativePath)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
        }

        public string Insert(Asset asset)
        {
            string relativePath = Path.Combine("assets", asset.RelativePath);
            string fullPath = GetFullPath(relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            using (FileStream fs = new FileStream(fullPath, FileMode.CreateNew))
            {
                fs.Write(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public async Task<string> InsertAsync(Asset asset, CancellationToken token = default(CancellationToken))
        {
            string relativePath = Path.Combine("assets", asset.RelativePath);
            string fullPath = GetFullPath(relativePath);
            using (System.IO.FileStream fs = new FileStream(fullPath, FileMode.CreateNew))
            {
                await fs.WriteAsync(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public string Update(Asset asset)
        {
            string relativePath = Path.Combine("assets", asset.RelativePath);
            string fullPath = GetFullPath(relativePath);
            using (System.IO.FileStream fs = new FileStream(fullPath, FileMode.Open))
            {
                fs.Write(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public async Task<string> UpdateAsync(Asset asset, CancellationToken token = default(CancellationToken))
        {
            string relativePath = asset.RelativePath;
            string fullPath = GetFullPath(relativePath);
            using (System.IO.FileStream fs = new FileStream(fullPath, FileMode.Open))
            {
                await fs.WriteAsync(asset.Contents, 0, asset.Contents.Length);
            }
            return relativePath;
        }

        public void Remove(string path)
        {
            string fullPath = GetFullPath(path);
            File.Delete(fullPath);
        }
    }
}
