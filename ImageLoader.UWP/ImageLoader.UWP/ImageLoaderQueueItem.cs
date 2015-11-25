using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Noear.UWP.Loader {
    public class ImageLoaderQueueItem {
        public string Key { get; internal set; }
        public string Url { get; internal set; }
        public DisplayImageOptions Options { get; internal set; }
        public Image View { get; internal set; }
        public ImageLoadingListener Listener { get; internal set; }
    }
}
