using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public class MemoryCache : IMemoryCache {
        int itemSize;
        public MemoryCache(int itemSize) {
            this.itemSize = itemSize;

            if (this.itemSize < 10)
                this.itemSize = 10;
        }
        
        Dictionary<int, BitmapImage> data = new Dictionary<int, BitmapImage>();
        public void Clear() {
            data.Clear();
        }

        public void Remove(string url) {
            var key = url.GetHashCode();
            data.Remove(key);
        }

        public BitmapImage Get(string url) {
            var key = url.GetHashCode();

            BitmapImage temp = null;
            data.TryGetValue(key, out temp);
            return temp;
        }

        public void Save(string url, BitmapImage image) {
            var key = url.GetHashCode();

            data[key] = image;
            if (data.Keys.Count > itemSize) {
                var temp = data.Keys.First();
                data.Remove(key);
            }
        }
    }
}
