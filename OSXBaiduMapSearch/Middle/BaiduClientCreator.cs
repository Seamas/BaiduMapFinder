using System;
using BaiduMap.Util;

namespace OSXBaiduMapSearch.Middle
{
    public static class BaiduClientCreator
    {
        public static BaiduClient Create()
        {
            return new BaiduClient("", "");
        }
    }
}
