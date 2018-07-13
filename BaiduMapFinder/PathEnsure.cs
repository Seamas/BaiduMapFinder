using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BaiduMapFinder
{
    public static class PathEnsure
    {
        public static void EnsurePath(string path)
        {
            var fileInfo = new FileInfo(path);
            var directory = fileInfo.Directory;
            if (!Directory.Exists(directory.FullName))
            {
                Directory.CreateDirectory(directory.FullName);
            }
        }
    }
}
