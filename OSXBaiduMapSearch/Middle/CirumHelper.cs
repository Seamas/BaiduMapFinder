using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using BaiduMap.Request;
using BaiduMap.Request.Models;
using BaiduMap.Response.Place;
using BaiduMap.Util;

namespace OSXBaiduMapSearch.Middle
{
    public class CirumHelper
    {

        private readonly double lat;
        private readonly double lng;
        private readonly int radius;
        private readonly string query;

        BaiduClient client;

        public event LogEvent LogEventHandler;

        public CirumHelper(string query, double lat, double lng, int radius)
        {
            this.query = query;
            this.lat = lat;
            this.lng = lng;
            this.radius = radius;
            client = BaiduClientCreator.Create();
        }

        public void Download(string fileName)
        {
            var index = 0;
            var req = BuildRequest(index);
            PlaceSearchResponse resp = client.Execute(req);
            Thread.Sleep(Constants.SLEEP);

            if (resp.Status == 0 && resp?.Results?.Count == 0)
            {
                LogEventHandler?.Invoke($"第 {index + 1} 页有 0 条数据" + Environment.NewLine);
            }
            else if (resp.Status == 0 && resp?.Results?.Count > 0)
            {
                if (resp?.Total == Constants.MAX_TOTAL)
                {
                    LogEventHandler?.Invoke($"本次查询结果数量为{resp.Total}条，百度地图最大限制为 {Constants.MAX_TOTAL} 条, 此次查询数据可能不全" + Environment.NewLine);
                }

                while (resp?.Results?.Count > 0)
                {
                    List<PlaceDetail> list = Mapper.Map<List<PlaceDetail>>(resp.Results);
                    LogEventHandler?.Invoke($"第 {index + 1} 页有 {list?.Count ?? 0} 条数据" + Environment.NewLine);
                    CsvWriter.Write(fileName, list);
                    req = BuildRequest(++index);
                    resp = client.Execute(req);
                    Thread.Sleep(Constants.SLEEP);
                }
            }

            if (resp?.Status != 0)
            {
                LogEventHandler?.Invoke($"查询第{index + 1}出错：" + resp?.Meta + Environment.NewLine);
            }
        }

        private PlaceSearchRequest<PlaceCircumModel> BuildRequest(int index)
        {
            var model = new PlaceCircumModel()
            {
                Query = query,
                Location = $"{lat},{lng}",
                Radius = radius > 0 ? radius.ToString() : "1000",
                Page_Num = index,
                Page_Size = Constants.PAGE_SIZE
            };

            return new PlaceSearchRequest<PlaceCircumModel>(model);
        }
    }
}
