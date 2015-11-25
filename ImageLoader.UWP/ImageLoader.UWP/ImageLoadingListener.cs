using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public delegate void ImageLoadingListener(LoadingState state, string url, Image view, BitmapImage image);
}
