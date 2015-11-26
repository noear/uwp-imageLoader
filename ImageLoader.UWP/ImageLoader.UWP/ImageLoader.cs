using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Noear.UWP.Loader {
    public class ImageLoader {
        /// <summary>
        /// 注意一个实例
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        public static ImageLoader Register(string key, ImageLoaderConfiguration config) {
            return ImageLoaders.Register(key, config);
        }
        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static ImageLoader Get(string key) {
            return ImageLoaders.Get(key);
        }
        /// <summary>
        /// 默认实例（需要独立初始化）
        /// </summary>
        public static ImageLoader Default {
            get { return ImageLoaders.Default; }
        }

        //---------//---------//---------//---------//---------//---------

        public IDiskCache DiskCache { get { return config.DiskCache; } }
        public IMemoryCache MemoryCache { get { return config.MemoryCache; } }

        protected ImageLoaderConfiguration config;
        protected Queue<ImageLoaderQueueItem> queue;
        protected int processing;

        public ImageLoader Init(ImageLoaderConfiguration config) {
            this.config = config;
            this.queue = new Queue<ImageLoaderQueueItem>();
            return this;
        }

        public void DisplayImage(string url, Image view) {
            DisplayImage(url, view, config.DisplayImageOptions, null);
        }

        public void DisplayImage(String uri, ImageBrush imageBrush) {
            DownloadImage(uri, (state, url, v, img) =>
             {
                 if (state == LoadingState.Completed) {
                     imageBrush.ImageSource = img;
                 }
             });
        }

        public void DisplayImage(string url, Image view, DisplayImageOptions options, ImageLoadingListener listener) {
            var item = new ImageLoaderQueueItem() { Url = url, Options = options, View = view, Listener = listener };
            add(item);
        }

        public void DownloadImage(string url, ImageLoadingListener listener) {
            DownloadImage(url, config.DisplayImageOptions, listener);
        }

        public void DownloadImage(string url, DisplayImageOptions options, ImageLoadingListener listener) {
            var item = new ImageLoaderQueueItem() { Url = url, Options = options, View = null, Listener = listener };
            add(item);
            tryStart();
        }
        //---------
        private void add(ImageLoaderQueueItem item) {
            queue.Enqueue(item);
            tryStart();//[触发1]每次添加都尝试启动任务
        }

        private async void tryStart() {
            if (processing < config.ThreadPoolSize) {
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
            if (item.Options.CacheInMemory) {
                var img = config.MemoryCache.Get(item.Url);
                if (img != null) {
                    doShow(item, img);
                    return;
                }
            }

            IBuffer buffer = null;
            BitmapImage image = null;
            if (item.Options.CacheOnDisk) {
                buffer = await config.DiskCache.Get(item.Url);
                image = await doDecode(item, buffer);
            }

            if (buffer == null) {
                buffer = await config.ImageDownloader.download(item.Url, item.Options.ExtraForDownloader);
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
                    if (image == null)
                        item.Listener(LoadingState.Failed, item.Url, item.View, image);
                    else
                        item.Listener(LoadingState.Completed, item.Url, item.View, image);
                });
            }
        }

        private void doSave(ImageLoaderQueueItem item, BitmapImage image, IBuffer buffer) {
            if (image == null || buffer == null)
                return;

            if (item.Options.CacheInMemory) {
                config.MemoryCache.Save(item.Url, image);
            }

            if (item.Options.CacheOnDisk) {
                config.DiskCache.Save(item.Url, buffer);
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
