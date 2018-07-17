using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OSXBaiduMapSearch.Middle
{
    public static class CsvWriter
    {
        public static void Write<T>(string fileName, IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return;
            }

            var properties = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
                .OrderBy(item => item.Name.ToLower());

            EnsurePath(fileName);

            if (!File.Exists(fileName))
            {
                var header = string.Join(",", properties.Select(item => item.Name.ToLower()));
                File.AppendAllLines(fileName, new List<string> { header });
            }

            var contents = enumerable.Select(value => string.Join(",", properties.Select(item => item.GetValue(value)?.ToString())));
            File.AppendAllLines(fileName, contents);
        }

        private static void EnsurePath(string path)
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
