using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public interface IMemoryCache {
        void Clear();
        void Delete(string key);
        void Save(string key, BitmapImage image);
        BitmapImage Get(string key);
    }
}
