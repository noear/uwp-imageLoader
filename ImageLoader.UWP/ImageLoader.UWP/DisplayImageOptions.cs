using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public class DisplayImageOptions {

        public bool CacheInMemory { get; private set; }
        public bool CacheOnDisk { get; private set; }
        public Object ExtraForDownloader { get; private set; }

        public class Builder{
            DisplayImageOptions options = new DisplayImageOptions();

            public Builder CacheInMemory(bool cacheInMemory) {
                options.CacheInMemory = cacheInMemory;
                return this;
            }

            public Builder CacheOnDisk(bool cacheOnDisk) {
                options.CacheOnDisk = cacheOnDisk;
                return this;
            }

            public Builder ExtraForDownloader(Object extraForDownloader) {
                options.ExtraForDownloader = extraForDownloader;
                return this;
            }

            public DisplayImageOptions Build() {
                return options;
            }
        }
    }
}
