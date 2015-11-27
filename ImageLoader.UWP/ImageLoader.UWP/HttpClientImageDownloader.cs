﻿using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Noear.UWP.Loader {
    public class HttpClientImageDownloader : IImageDownloader {
        public virtual Task<IBuffer> download(string url, object extra) {
            return new HttpClient().GetBufferAsync(new Uri(url)).AsTask();
        }
    }
}
