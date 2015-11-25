# uwp-imageLoader
UWP 自已的 ImageLoader （借签自iOS和Android上的接口设计）

初始化示例::
```java

ImageLoader.instance.init(new ImageLoaderConfiguration.Builder()
                                    .ThreadPoolSize(5)//线程池内加载的数量
                                    .DenyCacheImageMultipleSizesInMemory()
                                    .MemoryCache(new MemoryCache(10 * 1024 * 1024)) // 你可以通过自己的内存缓存实现
                                    .TasksProcessingOrder(QueueProcessingType.FIFO)
                                    .DiskCache(new DiskCache(ApplicationData.Current.LocalFolder, key_pics))//自定义缓存路径//100M
                                    .DefaultDisplayImageOptions(loaerOptions)
                                    .ImageDownloader(new HttpClientImageDownloader())
                                    .Build());

```

接口使用示例::
```java
var loader = ImageLoader.instance;

//a.1为imageView加载图片
loader.displayImage(uri, imageView);
//a.2为imageBrush加载图片源
loader.displayImage(uri, imageBrush);

//b.下载图片并回调
loader.downloadImage(uri, (state,url,view,img)=>{
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
ImageLoaders.Register("logo", new ImageLoaderConfiguration.Builder()
            .ThreadPoolSize(5)//线程池内加载的数量
            .DenyCacheImageMultipleSizesInMemory()
            .MemoryCache(new MemoryCache(10 * 1024 * 1024)) //你可以通过自己的内存缓存实现
            .TasksProcessingOrder(QueueProcessingType.FIFO)
            .DiskCache(new DiskCache(ApplicationData.Current.LocalFolder, "logo"))//自定义缓存路径//100M
            .DefaultDisplayImageOptions(loaerOptions)
            .ImageDownloader(new HttpClientImageDownloader())
            .Build());
```
2.添加命名空间，并使用
```xml
<page xmlns:loader="using:Noear.UWP.Loader">
  <loader:ImageView Src="{Binding logo}" Loader="logo" />
</page>
```
