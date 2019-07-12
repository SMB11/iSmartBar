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

namespace Infrastructure.Api
{
    public static class ApiImageHelper
    {
        //public const string ResourceBaseUrl = "http://coreapi-env.txt38iawzw.eu-west-3.elasticbeanstalk.com/";

        public static async Task<Bitmap> GetBitmapAsync(string url)
        {
            WebRequest request =
            WebRequest.Create(
            ConfigurationManager.AppSettings["ProductCdn"] + url);
            WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
            Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);
            return bitmap2;
        }

        public static async Task<byte[]> GetImageBytesAsync(string url, CancellationToken token = new CancellationToken())
        {
            Stream responseStream;
            try
            {
                responseStream = await (ConfigurationManager.AppSettings["ProductCdn"] + url).GetStreamAsync(token);
            }
            catch
            {
                token.ThrowIfCancellationRequested();
                return null;
            }
            MemoryStream stream = new MemoryStream();
            await responseStream.CopyToAsync(stream);
            token.ThrowIfCancellationRequested();
            return stream.ToArray();

        }
    }
}
