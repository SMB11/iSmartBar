using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Infrastructure
{
    public class CustomImageExtension : MarkupExtension
    {
        public string Image { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var bitmap = new BitmapImage();
            Bitmap bmp = Properties.Resources.ResourceManager.GetObject(Image) as Bitmap;
            if(bmp != null)
            {
                using (MemoryStream stream = new MemoryStream()) {
                    bmp.Save(stream, ImageFormat.Png);

                    stream.Position = 0;
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    bitmap.Freeze();
                }
            }
            return bitmap;
        }
    }
}
