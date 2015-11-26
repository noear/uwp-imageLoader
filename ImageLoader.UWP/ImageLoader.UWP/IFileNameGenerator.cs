using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public interface IFileNameGenerator {
        string Generate(string url);
    }
}
