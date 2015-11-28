using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public interface IMemoryCache {
        void Clear();
        void Remove(string url);
        void Save(string url, IBuffer buffer);
        IBuffer Get(string url);
    }
}
