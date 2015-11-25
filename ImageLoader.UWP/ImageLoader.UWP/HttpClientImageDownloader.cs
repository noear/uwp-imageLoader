using Noear.UWP.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public class HttpClientImageDownloader : IImageDownloader {

        protected virtual void build(AsyncHttpClient http, string url, object extra) {

        }

        public virtual async Task<IBuffer> download(string url, object extra) {
            var httpClient = new AsyncHttpClient().Url(url);

            build(httpClient, url, extra);

            var rsp = await httpClient.Get();

            return rsp.getBuffer();
        }
    }
}
