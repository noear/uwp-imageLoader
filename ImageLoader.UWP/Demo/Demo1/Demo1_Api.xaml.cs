using Noear.UWP.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Demo.Demo1 {
    
    public sealed partial class Demo_Api : Page {
        public Demo_Api() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            ImageLoader.Default.DisplayImage("http://x.x.x/x1.png", imageBrush);
            ImageLoader.Default.DisplayImage( "http://x.x.x/x2.png", imageView);
            ImageLoader.Default.DownloadImage("http://x.x.x/x2.png", (state, url, view, img) => {
                if (state == LoadingState.Completed) {
                    imageView2.Source = img;
                }
            });
        }
    }
}
