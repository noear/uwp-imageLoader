using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public class ImageLoader {
        protected ImageLoaderConfiguration config;
        protected Queue<ImageLoaderQueueItem> queue;
        protected int processing;

        public ImageLoader init(ImageLoaderConfiguration config) {
            this.config = config;
            this.queue = new Queue<ImageLoaderQueueItem>();
            return this;
        }

        public void displayImage(string url, Image view) {
            displayImage(url, view, config.displayImageOptions, null);
        }

        public void displayImage(string url, Image view, DisplayImageOptions options, ImageLoadingListener listener) {
            var item = new ImageLoaderQueueItem() { Url = url, Options = options, View = view, Listener = listener };
            add(item);
        }

        public void downloadImage(string url, ImageLoadingListener listener) {
            downloadImage(url, config.displayImageOptions, listener);
        }

        public void downloadImage(string url, DisplayImageOptions options, ImageLoadingListener listener) {
            var item = new ImageLoaderQueueItem() { Url = url, Options = options, View = null, Listener = listener };
            add(item);
            tryStart();
        }
        //---------
        private void add(ImageLoaderQueueItem item) {
            item.Key = Util.md5(item.Url);
            queue.Enqueue(item);
            tryStart();//[触发1]每次添加都尝试启动任务
        }
        
        private async void tryStart() {
            if (processing < config.threadPoolSize) {
                if (queue.Count > 0) {
                    processing++;
                    var item = queue.Dequeue();
                    await doProcess(item);
                    processing--;
                    tryStart();//[触发2]每完成一个任务后尝试启动新任何
                }
            }
        }

        private async Task doProcess(ImageLoaderQueueItem item) {
            if (item.Options.cacheInMemory) {
                var img = config.memoryCache.Get(item.Key);
                if (img != null) {
                    doShow(item, img);
                    return;
                }
            }

            IBuffer buffer = null;
            BitmapImage image = null;
            if (item.Options.cacheOnDisk) {
                buffer = await config.diskCache.Get(item.Key);
                image = await doDecode(item, buffer);
            }

            if (buffer == null) {
                buffer = await config.imageDownloader.download(item.Url, item.Options.extraForDownloader);
                image = await doDecode(item, buffer);
                doSave(item, image, buffer);
            }

            doShow(item, image);
        }

        private void doShow(ImageLoaderQueueItem item, BitmapImage image) {
            if (item.View != null && image != null) {
                Util.call(() =>
                {
                    item.View.Source = image;
                });

            }

            if (item.Listener != null) {
                Util.call(() =>
                {
                    if(image==null)
                        item.Listener(LoadingState.Failed, item.Url, item.View, image);
                    else
                        item.Listener(LoadingState.Completed, item.Url, item.View, image);
                });
            }
        }

        private void doSave(ImageLoaderQueueItem item, BitmapImage image, IBuffer buffer) {
            if (image == null || buffer == null)
                return;
            
            
            if (item.Options.cacheInMemory) {
                config.memoryCache.Save(item.Key, image);
            }

            if (item.Options.cacheOnDisk) {
                config.diskCache.Save(item.Key, buffer);
            }
        }

        private async Task<BitmapImage> doDecode(ImageLoaderQueueItem item, IBuffer buffer) {
            if (buffer != null) {

                InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream();
                DataWriter datawriter = new DataWriter(stream.GetOutputStreamAt(0));
                datawriter.WriteBuffer(buffer, 0, buffer.Length);
                await datawriter.StoreAsync();
                //await stream.WriteAsync(buffer);

                BitmapImage image = new BitmapImage();
                image.SetSource(stream);

                return image;
            }
            else {
                return null;
            }
        }
    }
}
