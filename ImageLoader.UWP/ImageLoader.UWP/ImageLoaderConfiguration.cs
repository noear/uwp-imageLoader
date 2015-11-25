using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public  class ImageLoaderConfiguration {
        public int threadPoolSize;
        public int threadPriority;
        public bool denyCacheImageMultipleSizesInMemory;
        public IMemoryCache memoryCache;
        public IDiskCache diskCache;
        public QueueProcessingType queueProcessingType;
        public DisplayImageOptions displayImageOptions;
        public IImageDownloader imageDownloader;

        
        public class Builder {

            private ImageLoaderConfiguration config = new ImageLoaderConfiguration();

            public Builder ThreadPoolSize(int threadPoolSize) {
                config.threadPoolSize = threadPoolSize;
                return this;
            }

            public Builder ThreadPriority(int threadPriority) {
                config.threadPriority = threadPriority;
                return this;
            }

            public Builder DenyCacheImageMultipleSizesInMemory() {
                config.denyCacheImageMultipleSizesInMemory = true;
                return this;
            }

            public Builder MemoryCache(IMemoryCache memoryCache) {
                config.memoryCache = memoryCache;
                return this;
            }

            public Builder TasksProcessingOrder(QueueProcessingType queueProcessingType) {
                config.queueProcessingType = queueProcessingType;
                return this;
            }

            public Builder DiskCache(IDiskCache diskCache) {
                config.diskCache = diskCache;
                return this;
            }

            public Builder DefaultDisplayImageOptions(DisplayImageOptions displayImageOptions) {
                config.displayImageOptions = displayImageOptions;
                return this;
            }

            public Builder ImageDownloader(IImageDownloader imageDownloader) {
                config.imageDownloader = imageDownloader;
                return this;
            }

            public ImageLoaderConfiguration Build() {
                return config;
            }
        }
    }
}
