# uwp-imageLoader
UWP 自已的 ImageLoader （借签自iOS和Android上的接口设计）

```java
//配置
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

控件的使用
```xml
<page xmlns:loader="using:Noear.UWP.Loader">
  <loader:ImageView Src="{Binding logo}" Loader="logo" />
</page>
```
