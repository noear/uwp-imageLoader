using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public class MemoryCache : IMemoryCache {

        public MemoryCache() {

        }

        public MemoryCache(int limitSize) {

        }

        Dictionary<int, BitmapImage> data = new Dictionary<int, BitmapImage>();
        public void Clear() {
            data.Clear();
        }

        public void Remove(string url) {
            data.Remove(url.GetHashCode());
        }

        public BitmapImage Get(string url) {
            return data[url.GetHashCode()];
        }

        public void Save(string url, BitmapImage image) {
            data[url.GetHashCode()] = image;
        }
    }
}
