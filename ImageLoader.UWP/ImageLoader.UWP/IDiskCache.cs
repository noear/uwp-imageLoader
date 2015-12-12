using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Noear.UWP.Loader {
    public interface IDiskCache {
        Task Clear();
        Task Remove(string url);
        Task<bool> Save(string url, IBuffer buffer);
        Task<IBuffer> Get(string url);
    }
}
