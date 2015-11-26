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

namespace Demo.Demo2 {
    
    public sealed partial class Demo_Api : Page {
        public Demo_Api() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            ImageUtil2.Icon.DisplayImage("http://x.x.x/x1.png", iconView);
            ImageUtil2.Photo.DisplayImage("http://x.x.x/x2.png", photoView);
        }

        public void demo2_ex() {
            ImageUtil2.DownloadPhoto("http://x.x.x/x2.png", (img) =>
            {
                photoView.Source = img;
            });
        }
    }


}
