using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noear.UWP.Loader {
    public class Md5FileNameGenerator : IFileNameGenerator {
        public string Generate(string url) {
            return Util.md5(url);
        }
    }
}
