using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Common.Core
{
    public static class ImageHelper
    {

        public static string GetImageExtension(byte[] bytes)
        {
            Bitmap bitmap = ToImage(bytes);
            string ext = null;
            if (bitmap.RawFormat.Equals(ImageFormat.Bmp)) ext = "bmp";
            else if (bitmap.RawFormat.Equals(ImageFormat.Jpeg)) ext = "jpg";
            else if (bitmap.RawFormat.Equals(ImageFormat.Png)) ext = "png";
            return ext;
        }


        private static Bitmap ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new Bitmap(ms);
                return image;
            }
        }
    }
}
