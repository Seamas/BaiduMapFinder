using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaiduMap.Request;
using BaiduMap.Request.Models;
using BaiduMap.Response.Place;
using BaiduMap.Util;

namespace OSXBaiduMapSearch.Middle
{
    public class RectangeHelper
    {
        private double? leftLat;
        private double? leftLng;
        private double? rightLat;
        private double? rightLng;
        private string query;
        private BaiduClient client;

        public event LogEvent LongEventHandler;

        public RectangeHelper(string query, double? leftLat, double? leftLng, double? rightLat, double? rightLng)
        {
            this.query = query;
            this.leftLat = leftLat;
            this.leftLng = leftLng;
            this.rightLat = rightLat;
            this.rightLng = rightLng;
            this.client = BaiduClientCreator.Create();
        }

        public void Download(string fileName)
        {
            Download(leftLat, rightLat, leftLng, rightLng, fileName);
        }

        private void SplitRectange(double? lat1, double? lat2, double? lng1, double? lng2, string fileName, int xSplit, int ySplit)
        {
            if (xSplit < 1) { xSplit = 1; }
            if (ySplit < 1) { ySplit = 1; }

            for (var i = 1; i <= xSplit; i++)
            {
                var lastLat = lat1 + (lat2 - lat1) / xSplit * (i - 1);
                var currentLat = lat1 + (lat2 - lat1) / xSplit * i;
                for (var j = 1; j <= ySplit; j++)
                {
                    var lastLng = lng1 + (lng2 - lng1) / ySplit * (j - 1);
                    var currentLng = lng1 + (lng2 - lng1) / ySplit * i;
                    Download(lastLat, currentLat, lastLng, currentLng, fileName);
                }
            }
        }

        private void Download(double? lastLat, double? currentLat, double? lastLng, double? currentLng, string fileName)
        {
            var index = 0;

            var req = BuildRequest(lastLat, lastLng, currentLat, currentLng, index);

            LongEventHandler?.Invoke($"正在下载 {index + 1} 页，矩形区域：{lastLat},{lastLng}, -- {currentLat},{currentLng}  关键字: {query}" + Environment.NewLine);

            PlaceSearchResponse resp = client.Execute(req);
            Thread.Sleep(Constants.SLEEP);

            if (resp?.Total == Constants.MAX_TOTAL)
            {
                LongEventHandler?.Invoke($"矩形区域：{lastLat},{lastLng}, -- {currentLat},{currentLng}  关键字: {query} 结果>={Constants.MAX_TOTAL}条，进行区域拆分查询");
                SplitRectange(lastLat, currentLat, lastLng, currentLng, fileName, 2, 2);
            }
            else if (resp.Status == 0 && resp?.Results?.Count == 0)
            {
                LongEventHandler?.Invoke($"第 {index + 1} 页共有 0 条数据" + Environment.NewLine);
            }
            else if (resp.Status == 0 && resp?.Results?.Count > 0)
            {
                LongEventHandler?.Invoke($"本区域查询结果数量为{resp.Total}条" + Environment.NewLine);

                while (resp?.Results?.Count > 0)
                {
                    List<PlaceDetail> list = Mapper.Map<List<PlaceDetail>>(resp.Results);
                    LongEventHandler?.Invoke($"第 {index + 1} 页共有 {list?.Count ?? 0} 条数据" + Environment.NewLine);
                    CsvWriter.Write(fileName, list);
                    req = BuildRequest(lastLat, lastLng, currentLat, currentLng, ++index);
                    resp = client.Execute(req);
                    Thread.Sleep(Constants.SLEEP);
                }
            }

            if (resp.Status != 0)
            {
                LongEventHandler?.Invoke($"第 {index + 1} 页查询出错：" + resp.Meta);
            }
        }

        private PlaceSearchRequest<PlaceRectangeModel> BuildRequest(double? lat1, double? lng1, double? lat2, double? lng2, int index)
        {
            var model = new PlaceRectangeModel()
            {
                Query = query,
                Bounds = $"{(lat1 > lat2 ? lat2 : lat1)},{(lng1 > lng2 ? lng2 : lng1)},{(lat2 < lat1 ? lat1 : lat2)},{(lng2 < lng1 ? lng1 : lng2)}",
                Page_Size = Constants.PAGE_SIZE,
                Page_Num = index
            };
            return new PlaceSearchRequest<PlaceRectangeModel>(model);
        }
    }
}
