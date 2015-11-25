using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
     public class ImageLoaders {
        static Dictionary<string, ImageLoader> _libs = new Dictionary<string, ImageLoader>();
        public static ImageLoader Register(string key, ImageLoaderConfiguration config) {
            var temp = new ImageLoader().Init(config);
            _libs[key] = temp;
            return temp;
        }

        public static ImageLoader Get(string key) {
            return _libs[key];
        }
    }
}
