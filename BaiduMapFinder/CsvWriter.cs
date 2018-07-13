using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace BaiduMapFinder
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

            PathEnsure.EnsurePath(fileName);
            
            if (!File.Exists(fileName))
            {
                var header = string.Join(",", properties.Select(item => item.Name.ToLower()));
                File.AppendAllLines(fileName, new List<string> { header });
            }

            var contents = enumerable.Select(value => string.Join(",", properties.Select(item => item.GetValue(value)?.ToString())));
            File.AppendAllLines(fileName, contents);
        }
    }
}
