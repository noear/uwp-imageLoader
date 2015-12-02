using Noear.UWP.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Noear.UWP.Http;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace Demo.Demo2 {
    public static class ImageUtil2 {

        public static ImageLoader Icon { get; private set; }
        public static ImageLoader Photo { get; private set; }

        //请在APP起动的时候调用
        public async static void TryInit() {
            var options = new DisplayImageOptions.Builder()
                .CacheInMemory(false)
                .CacheOnDisk(true)
                .Build();

            var root = ApplicationData.Current.LocalFolder;
            //图标
            var iconDir = await root.CreateFolderAsync("icon", CreationCollisionOption.OpenIfExists);
            Icon = ImageLoader.Register("icon", new ImageLoaderConfiguration.Builder()
                                .ThreadPoolSize(5)
                                .TasksProcessingOrder(QueueProcessingType.FIFO)
                                .DiskCache(new DiskCache(iconDir, new Md5FileNameGenerator()))
                                .DefaultDisplayImageOptions(options)
                                .ImageDownloader(new HttpClientImageDownloader())
                                .Build());

            //像册
            var photoDir = await root.CreateFolderAsync("photo", CreationCollisionOption.OpenIfExists);
            Photo = ImageLoader.Register("photo", new ImageLoaderConfiguration.Builder()
                                .ThreadPoolSize(5)
                                .TasksProcessingOrder(QueueProcessingType.FIFO)
                                .DiskCache(new DiskCache(photoDir, new Md5FileNameGenerator()))
                                .DefaultDisplayImageOptions(options)
                                .ImageDownloader(new HttpClientImageDownloaderEx()) //使用自定义扩展支持高级用法
                                .Build());
        }

        //===============
        //以下是高级用户
        public static void DownloadPhoto(string url, Action<BitmapImage> callback) {
            var user = new User() { UserID = "1", Sex = "0" }; //可以从本地Session处取得

            var options = new DisplayImageOptions.Builder()
                .CacheInMemory(false)
                .CacheOnDisk(true)
                .ExtraForDownloader(user)
                .Build();

            Photo.DownloadImage(url, options, (state, uri, view, img) =>
            {
                if (state == LoadingState.Completed) {
                    callback(img);
                }
            });
        }

        public class User {
            public string UserID;
            public string Sex;
        }

        class HttpClientImageDownloaderEx : IImageDownloader {
            public async Task<IBuffer> download(string url, object extra) {
                AsyncHttpClient http = new AsyncHttpClient();
                http.Url(url);
                if (extra != null) {
                    //把相关信息传过去作防盗验证
                    var user = extra as User;
                    http.Header("UserID", user.UserID);
                    http.Header("Sex", user.Sex);
                    http.Header("APPID", "1111");
                    http.Header("APPMK", "XXXXXXXXXXXXXXXX");
                }

                var rsp = await http.Get();
                return rsp.getBuffer();
            }
        }
    }
}
