using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;

namespace Infrastructure.Utility
{
    public static class ImageHelper
    {
        public static async Task<Bitmap> DownloadBitmapAsync(string url)
        {

            WebRequest request =
            WebRequest.Create( url);
            WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
            Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);
            return bitmap2;
        }

        public static async Task<byte[]> DownloadBytesAsync(string url, CancellationToken token = new CancellationToken())
        {
            Stream responseStream;
            try
            {
                responseStream = await (url).GetStreamAsync(token).ConfigureAwait(false);
            }
            catch
            {
                token.ThrowIfCancellationRequested();
                return null;
            }
            MemoryStream stream = new MemoryStream();
            await responseStream.CopyToAsync(stream).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            return stream.ToArray();

        }
    }
}
