using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
     internal static class ImageLoaders {
        static Dictionary<string, ImageLoader> _libs = new Dictionary<string, ImageLoader>();
        public static ImageLoader Register(string key, ImageLoaderConfiguration config) {
            var temp = new ImageLoader().Init(config);
            _libs[key] = temp;
            return temp;
        }

        public static ImageLoader Get(string key) {
            return _libs[key];
        }

        static ImageLoader _Default;
        public static ImageLoader Default {
            get {
                if (_Default == null) {
                    _Default = new ImageLoader();
                }

                return _Default;
            }
        }
    }
}
