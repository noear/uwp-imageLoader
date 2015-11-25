using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Noear.UWP.Loader {
    public sealed partial class ImageView : UserControl {
        public ImageView() {
            this.InitializeComponent();
        }
        
        public static DependencyProperty StretchProperty { get; } = DependencyProperty.Register(
            nameof(Stretch),
            typeof(Stretch),
            typeof(ImageView),
            new PropertyMetadata(Stretch.None)
            );

        public static DependencyProperty SrcProperty { get; } = DependencyProperty.Register(
            nameof(Src),
            typeof(Uri),
            typeof(ImageView),
            new PropertyMetadata(null, new PropertyChangedCallback(OnSrcPropertyChanged))
            );

        private static DependencyProperty IsLoadingProperty { get; } = DependencyProperty.Register(
            nameof(IsLoading),
            typeof(bool),
            typeof(ImageView),
            new PropertyMetadata(false)
            );

        private static DependencyProperty LoaderProperty { get; } = DependencyProperty.Register(
            nameof(Loader),
            typeof(string),
            typeof(ImageView),
            new PropertyMetadata(null)
            );

        /// <summary>
        /// 用于定义当前的ImageLoader
        /// </summary>
        public string Loader {
            get { return GetValue(LoaderProperty) as string; }
            set { SetValue(LoaderProperty, value); }
        }


        public bool IsLoading {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public Stretch Stretch {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        public Uri Src {
            get { return (Uri)GetValue(SrcProperty); }
            set { SetValue(SrcProperty, value); }
        }
        //-------

        ImageLoader _CurrentLoader;
        public ImageLoader CurrentLoader() {
            if (_CurrentLoader == null) {
                _CurrentLoader = ImageLoaders.Get(Loader);
            }

            return _CurrentLoader;
        }

        private static void OnSrcPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var wseft = d as ImageView;
            wseft?.DoUpdateSourceAsync();
        }

        private void DoUpdateSourceAsync() {
            if (Src == null || Src.Scheme == null)
                return;

            if (Src.Scheme.IndexOf("http") < 0) {//如果不是http地址
                view.Source = new BitmapImage(Src);
            }
            else {//如果是http地址
                var loader = CurrentLoader();
                loader.DownloadImage(Src.AbsoluteUri, (state, url, v, image) =>
                {
                    if (_isLoaded) {
                        view.Source = image;
                    }
                });
            }
        }

        //-------

        bool _isLoaded = false;

        private void view_Loaded(object sender, RoutedEventArgs e) {
            _isLoaded = true;
        }

        private void view_Unloaded(object sender, RoutedEventArgs e) {
            _isLoaded = false;
        }
    }
}
