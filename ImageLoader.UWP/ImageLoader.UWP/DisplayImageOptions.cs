using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public class DisplayImageOptions {

        public bool cacheInMemory { get; private set; }
        public bool cacheOnDisk { get; private set; }
        public Object extraForDownloader { get; private set; }

        public class Builder{
            DisplayImageOptions options = new DisplayImageOptions();

            public Builder cacheInMemory(bool cacheInMemory) {
                options.cacheInMemory = cacheInMemory;
                return this;
            }

            public Builder cacheOnDisk(bool cacheOnDisk) {
                options.cacheOnDisk = cacheOnDisk;
                return this;
            }

            public Builder extraForDownloader(Object extraForDownloader) {
                options.extraForDownloader = extraForDownloader;
                return this;
            }

            public DisplayImageOptions build() {
                return options;
            }
        }
    }
}
