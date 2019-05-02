﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Api
{
    public static class ApiImageHelper
    {
        //public const string ResourceBaseUrl = "https://localhost:44396/";
        public const string ResourceBaseUrl = "http://coreapi-env.txt38iawzw.eu-west-3.elasticbeanstalk.com/";

        public static async Task<Bitmap> GetBitmapAsync(string url)
        {
            WebRequest request =
            WebRequest.Create(
            ResourceBaseUrl + url);
            WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
            Stream responseStream =
                response.GetResponseStream();
            Bitmap bitmap2 = new Bitmap(responseStream);
            return bitmap2;
        }

        public static async Task<byte[]> GetImageBytesAsync(string url, CancellationToken token = new CancellationToken())
        {
            WebRequest request =
            WebRequest.Create(
            ResourceBaseUrl + url);
            WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            Stream responseStream =
                response.GetResponseStream();
            MemoryStream streamRes = new MemoryStream();
            await responseStream.CopyToAsync(streamRes, 81920, token).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            return streamRes.ToArray();
        }
    }
}
