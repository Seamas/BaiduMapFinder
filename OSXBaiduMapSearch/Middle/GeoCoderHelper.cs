using System;
using BaiduMap.Request;
using BaiduMap.Request.Models;
using BaiduMap.Util;

namespace OSXBaiduMapSearch.Middle
{
    public class GeoCoderHelper
    {
        private string address;
        private string city;

        private BaiduClient baiduClient;

        public GeoCoderHelper(string address, string city)
        {
            this.address = address;
            this.city = city;
            baiduClient = BaiduClientCreator.Create();
        }

        private GeoCoderRequest BuildRequest()
        {
            var model = new GeoCoderModel()
            {
                Address = address,
                City = city
            };

            return new GeoCoderRequest(model);
        }

        public string Query()
        {
            var req = BuildRequest();
            return baiduClient.ExecuteReadString(req);
        }
    }
}
