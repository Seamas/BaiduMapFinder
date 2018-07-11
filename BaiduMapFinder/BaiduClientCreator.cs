using BaiduMap.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduMapFinder
{
    public static class BaiduClientCreator
    {
        public static BaiduClient Create()
        {
            return new BaiduClient(Setting.Instance.Ak, Setting.Instance.Sk);
        }
    }
}
