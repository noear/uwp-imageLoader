using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public class DiskCache : IDiskCache {
        public DiskCache() {
        }

        public DiskCache(int limitSize) {
        }

        StorageFolder root;
        StorageFolder folder;
        string directory;
        public DiskCache(StorageFolder root, string directory) {
            this.root = root;
            this.directory = directory;
            doInit();
        }

        async void doInit() {
            folder = await root.CreateFolderAsync(directory, CreationCollisionOption.OpenIfExists).AsTask();
        }

        public async void Clear() {
            await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
            doInit();//重新生成文件夹
        }

        public async Task<IBuffer> Get(string key) {
            var item = await folder.TryGetItemAsync(key);
            if (item != null) {
                var file = item as StorageFile;
                if (file != null) {
                    return await FileIO.ReadBufferAsync(file);
                }
            }

            return null;
        }

        public async void Delete(string key) {
            var file = await folder.TryGetItemAsync(key);
            if (file != null) {
                await file.DeleteAsync();
            }
        }

        public async void Save(string key, IBuffer buffer) {
            var file = await folder.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(file, buffer);
        }
    }
}
