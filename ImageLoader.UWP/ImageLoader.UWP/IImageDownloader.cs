using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Windows.Web.Http;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public interface IImageDownloader {
        Task<IBuffer> download(string url, Object extra);
    }
}
