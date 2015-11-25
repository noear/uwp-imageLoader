using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Noear.UWP.Loader {
    internal static class Util {
        public static String md5(String code) {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(code, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            return CryptographicBuffer.EncodeToHexString(hashed);

        }

        public static async void asynCall(int delayMillis, DispatchedHandler fun) {
            if (delayMillis > 0) {
                await Task.Delay(delayMillis);
            }

            call(fun);
        }

        public static void asynCall(DispatchedHandler fun) {
            asynCall(10, fun);
        }

        public async static void call(DispatchedHandler fun) {
            if (Window.Current.Dispatcher.HasThreadAccess)
                fun.Invoke();
            else
                await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, fun);
        }
    }
}
