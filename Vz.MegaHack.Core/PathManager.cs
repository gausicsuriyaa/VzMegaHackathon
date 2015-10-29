using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vz.MegaHack.Core {
    public static class PathManager {
        public static string DataPath {
            get;
            private set;
        }

        public static void Initialize(string dataPath) {
            DataPath = dataPath;
        }
    }
}
