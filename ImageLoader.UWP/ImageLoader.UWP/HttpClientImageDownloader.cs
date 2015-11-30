using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Noear.UWP.Loader {
    public class HttpClientImageDownloader : IImageDownloader {
        public async Task<IBuffer> download(string url, object extra) {
            try {
                return await new HttpClient().GetBufferAsync(new Uri(url));
            }
            catch {
                return null;
            }
        }
    }
}
