# uwp-imageLoader
UWP 自已的 ImageLoader （借签自iOS和Android上的接口设计）

详细示例请参考Demo项目！！！（Demo1为简易示例；Demo2为高级示例）

1.1.初始化示例::
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

1.2.接口使用示例::
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

1.3控件使用示例::

```xml
<page xmlns:loader="using:Noear.UWP.Loader">
  <loader:ImageView Src="{Binding icon_url}"  />
</page>
```

更多高级操作请参考Demo项目


