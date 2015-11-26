# uwp-imageLoader
UWP 自已的 ImageLoader （借签自iOS和Android上的接口设计）

更详细示例请参考Demo项目

初始化示例::
```java

var options = new DisplayImageOptions.Builder()
    .CacheInMemory(false)
    .CacheOnDisk(true)
    .Build();
            
ImageLoader.Default.Init(new ImageLoaderConfiguration.Builder()
                    .ThreadPoolSize(5)
                    .DenyCacheImageMultipleSizesInMemory()
                    .MemoryCache(new MemoryCache(10 * 1024 * 1024))
                    .TasksProcessingOrder(QueueProcessingType.FIFO)
                    .DiskCache(new DiskCache(ApplicationData.Current.LocalFolder, new Md5FileNameGenerator()))
                    .DefaultDisplayImageOptions(options)
                    .ImageDownloader(new HttpClientImageDownloader())
                    .Build());

```

接口使用示例::
```java
var loader = ImageLoader.Default;

//a.1为imageView加载图片
loader.DisplayImage(uri, imageView);
//a.2为imageBrush加载图片源
loader.DisplayImage(uri, imageBrush);

//b.下载图片并回调
loader.DownloadImage(uri, (state,url,view,img)=>{
    if (state == LoadingState.Completed)
        ...
    else if (state == LoadingState.Failed)
        ...
    else if (state == LoadingState.Cancelled)
        ...
});
```

控件使用示例::

1.注册Loader
```java
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
    Icon = ImageLoader.Register("icon",new ImageLoaderConfiguration.Builder()
                        .ThreadPoolSize(5)
                        .DenyCacheImageMultipleSizesInMemory()
                        .MemoryCache(new MemoryCache(10 * 1024 * 1024))
                        .TasksProcessingOrder(QueueProcessingType.FIFO)
                        .DiskCache(new DiskCache(iconDir, 100 * 1024 * 1024, new Md5FileNameGenerator()))
                        .DefaultDisplayImageOptions(options)
                        .ImageDownloader(new HttpClientImageDownloader())
                        .Build());

    //像册
    var photoDir = await root.CreateFolderAsync("photo", CreationCollisionOption.OpenIfExists);
    Photo = ImageLoader.Register("photo", new ImageLoaderConfiguration.Builder()
                        .ThreadPoolSize(5)
                        .DenyCacheImageMultipleSizesInMemory()
                        .MemoryCache(new MemoryCache(10 * 1024 * 1024))
                        .TasksProcessingOrder(QueueProcessingType.FIFO)
                        .DiskCache(new DiskCache(photoDir, new Md5FileNameGenerator()))
                        .DefaultDisplayImageOptions(options)
                        .ImageDownloader(new HttpClientImageDownloaderEx()) //使用自定义扩展支持高级用法
                        .Build());
}
```
2.添加命名空间，并使用
```xml
<page xmlns:loader="using:Noear.UWP.Loader">
  <loader:ImageView Src="{Binding icon_url}" Loader="icon" />
</page>
```
