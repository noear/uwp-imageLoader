using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public class MemoryCache : IMemoryCache {
        uint limitSize;
        public MemoryCache(uint limitSize) {
            this.limitSize = limitSize;
        }

        List<int> keyList = new List<int>();
        uint data_size = 0;//字节的数量
        Dictionary<int, IBuffer> data = new Dictionary<int, IBuffer>();
        public void Clear() {
            data.Clear();
            data_size = 0;
            keyList.Clear();
        }

        public void Remove(string url) {
            var key = url.GetHashCode();

            DoRemove(key);
        }

        private void DoRemove(int key) {
            IBuffer temp = null;
            data.TryGetValue(key, out temp);
            if (temp != null) {
                data_size -= temp.Length;
                data.Remove(key);
                keyList.Remove(key);
            }
        }
        
        public IBuffer Get(string url) {
            var key = url.GetHashCode();

            IBuffer temp = null;
            data.TryGetValue(key, out temp);
            return temp;
        }

        public void Save(string url, IBuffer buffer) {
            var key = url.GetHashCode();

            DoRemove(key);

            keyList.Add(key);
            data.Add(key, buffer);
            data_size += buffer.Length;

            if (data_size > limitSize) {
                var temp = keyList[0];
                DoRemove(temp);
            }
        }
    }
}
