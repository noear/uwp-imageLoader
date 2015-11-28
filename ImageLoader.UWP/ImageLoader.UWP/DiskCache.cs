using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public class DiskCache : IDiskCache {
        
        StorageFolder root;
        StorageFolder folder;
        string directory;
        IFileNameGenerator fileNameGenerator;
        protected int limitSize;
        public DiskCache(StorageFolder root, IFileNameGenerator fileNameGenerator)
        {
            this.root = root;
            this.directory = "image";
            this.fileNameGenerator = fileNameGenerator;
            this.limitSize = 0;
            doInit();
        }
        
        async void doInit() {
            folder = await root.CreateFolderAsync(directory, CreationCollisionOption.OpenIfExists);
        }

        public async void Clear() {
            await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
            doInit();//重新生成文件夹
        }

        public async Task<IBuffer> Get(string url) {
            string name = getFileName(url);
            string path = name.Substring(0, 2);

            var dir  = await folder.CreateFolderAsync(path, CreationCollisionOption.OpenIfExists);

            var item = await dir.TryGetItemAsync(name);
            if (item != null) {
                var file = item as StorageFile;
                if (file != null) {
                    return await FileIO.ReadBufferAsync(file);
                }
            }

            return null;
        }

        public async void Remove(string url) {
            string name = getFileName(url);
            string path = name.Substring(0, 2);

            var dir = await folder.CreateFolderAsync(path, CreationCollisionOption.OpenIfExists);

            var file = await dir.TryGetItemAsync(name);
            if (file != null) {
                await file.DeleteAsync();
            }
        }

        public async void Save(string url, IBuffer buffer) {
            string name = getFileName(url);
            string path = name.Substring(0, 2);

            var dir = await folder.CreateFolderAsync(path, CreationCollisionOption.OpenIfExists);

            var file = await dir.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(file, buffer);
        }

        string getFileName(string url) {
            return fileNameGenerator.Generate(url);
        }
    }
}
