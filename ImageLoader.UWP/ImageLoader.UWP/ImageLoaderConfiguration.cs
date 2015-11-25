using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public  class ImageLoaderConfiguration {
        public int ThreadPoolSize;
        public int ThreadPriority;
        public bool DenyCacheImageMultipleSizesInMemory;
        public IMemoryCache MemoryCache;
        public IDiskCache DiskCache;
        public QueueProcessingType QueueProcessingType;
        public DisplayImageOptions DisplayImageOptions;
        public IImageDownloader ImageDownloader;

        
        public class Builder {

            private ImageLoaderConfiguration config = new ImageLoaderConfiguration();

            public Builder ThreadPoolSize(int threadPoolSize) {
                config.ThreadPoolSize = threadPoolSize;
                return this;
            }

            public Builder ThreadPriority(int threadPriority) {
                config.ThreadPriority = threadPriority;
                return this;
            }

            public Builder DenyCacheImageMultipleSizesInMemory() {
                config.DenyCacheImageMultipleSizesInMemory = true;
                return this;
            }

            public Builder MemoryCache(IMemoryCache memoryCache) {
                config.MemoryCache = memoryCache;
                return this;
            }

            public Builder TasksProcessingOrder(QueueProcessingType queueProcessingType) {
                config.QueueProcessingType = queueProcessingType;
                return this;
            }

            public Builder DiskCache(IDiskCache diskCache) {
                config.DiskCache = diskCache;
                return this;
            }

            public Builder DefaultDisplayImageOptions(DisplayImageOptions displayImageOptions) {
                config.DisplayImageOptions = displayImageOptions;
                return this;
            }

            public Builder ImageDownloader(IImageDownloader imageDownloader) {
                config.ImageDownloader = imageDownloader;
                return this;
            }

            public ImageLoaderConfiguration Build() {
                return config;
            }
        }
    }
}
