using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Infrastructure
{
    public class CustomImageExtension : MarkupExtension
    {
        public string Image { get; set; }

        private static BitmapSource emptyImage;

        private static BitmapSource EmptyImage
        {
            get
            {
                if (emptyImage == null)
                {
                    int width = 128;
                    int height = width;
                    int stride = width / 8;
                    byte[] pixels = new byte[height * stride];

                    // Try creating a new image with a custom palette.
                    List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
                    colors.Add(System.Windows.Media.Colors.Red);
                    colors.Add(System.Windows.Media.Colors.Blue);
                    colors.Add(System.Windows.Media.Colors.Green);
                    BitmapPalette myPalette = new BitmapPalette(colors);

                    // Creates a new empty image with the pre-defined palette
                    emptyImage = BitmapSource.Create(
                                                             width, height,
                                                             96, 96,
                                                             PixelFormats.Indexed1,
                                                             myPalette,
                                                             pixels,
                                                             stride);
                }
                return emptyImage;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var bitmap = new BitmapImage();
            byte[] bmp = global::Infrastructure.Core.Properties.Resources.ResourceManager.GetObject(Image) as byte[];
            if (bmp != null)
            {
                using (MemoryStream stream = new MemoryStream(bmp))
                {


                    stream.Position = 0;
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    bitmap.Freeze();
                }
            }
            else
                return EmptyImage;
            return bitmap;
        }
    }
}
