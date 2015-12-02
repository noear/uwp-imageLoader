using Noear.UWP.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Demo.Demo1 {
    public class ImageUtil1 {
        //请在APP起动的时候调用
        public static void TryInit() {
            var options = new DisplayImageOptions.Builder()
                .CacheInMemory(false)
                .CacheOnDisk(true)
                .Build();
            
            ImageLoader.Default.Init(new ImageLoaderConfiguration.Builder()
                               .ThreadPoolSize(5)
                               .TasksProcessingOrder(QueueProcessingType.FIFO)
                               .DiskCache(new DiskCache(ApplicationData.Current.LocalFolder, new Md5FileNameGenerator()))
                               .DefaultDisplayImageOptions(options)
                               .ImageDownloader(new HttpClientImageDownloader())
                               .Build());
        }
    }
}
