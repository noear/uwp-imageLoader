using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader
{
    public interface IMemoryCache
    {
        void Clear();
        void Remove(string url);
        void Save(string url, BitmapImage image);
        BitmapImage Get(string url);
    }
}
