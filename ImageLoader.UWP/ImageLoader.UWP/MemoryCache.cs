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

        Dictionary<string, BitmapImage> data = new Dictionary<string, BitmapImage>();
        public void Clear() {
            data.Clear();
        }

        public void Delete(string key) {
            data.Remove(key);
        }

        public BitmapImage Get(string key) {
            return data[key];
        }

        public void Save(string key, BitmapImage image) {
            data[key] = image;
        }
    }
}
