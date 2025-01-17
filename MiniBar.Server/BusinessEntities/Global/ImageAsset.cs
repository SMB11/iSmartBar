﻿using Common.Core;
using System;

namespace BusinessEntities.Global
{
    public class ImageAsset : Asset
    {
        public const int ResizedImageHeight = 400;

        public ImageAsset(byte[] content) : base(content)
        {
        }

        public ImageAsset(byte[] content, string path) : base(content, path)
        {
        }

        protected override byte[] ProcessContent(byte[] content)
        {
            return ImageHelper.ResizeImage(content, ResizedImageHeight);
        }

        public override string ContentType { get => "image/jpeg"; }
    }
}
