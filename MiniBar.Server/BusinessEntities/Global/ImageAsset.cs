using Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Global
{
    public class ImageAsset : Asset
    {
        public const int ResizedImageHeight = 400;

        public ImageAsset(byte[] image)
        {
            this.Contents = ResizeImage(image);
            BuildRelativePath();
        }

        public ImageAsset(byte[] image, string path)
        {
            this.Contents = ResizeImage(image);
            RelativePath = path;
        }

        private void BuildRelativePath()
        {
            RelativePath = "images/" + Guid.NewGuid() + ".jpg";
        }

        private byte[] ResizeImage(byte[] image)
        {
            return ImageHelper.ResizeImage(image, ResizedImageHeight);
        }
    }
}
