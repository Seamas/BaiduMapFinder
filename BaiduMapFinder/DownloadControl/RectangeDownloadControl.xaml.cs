using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaiduMap.Request.Models;
using BaiduMap.Request;
using Newtonsoft.Json;
using BaiduMap.Response.Place;
using AutoMapper;

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for RectangeQuery.xaml
    /// </summary>
    public partial class RectangeDownloadControl : UserControl
    {
        
        public RectangeDownloadControl()
        {
            InitializeComponent();
        }

        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            btnQuery.IsEnabled = false;
            txbResult.Clear();

            var lat1 = location1.Location.Lat;
            var lng1 = location1.Location.Lng;
            var lat2 = location2.Location.Lat;
            var lng2 = location2.Location.Lng;
            var query = txbKey.Text.Trim();

            int.TryParse(txbLat.Text.Trim(), out int x);
            int.TryParse(txbLng.Text.Trim(), out int y);
            string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download", $"{lng1}_{lat1}_{query}_{lng2}_{lat2}.csv");
            await SplitRectange(query, lat1, lat2, lng1, lng2, fileName, x, y);
            btnQuery.IsEnabled = true;
        }

        private async Task SplitRectange(string query, double? lat1, double? lat2, double? lng1, double? lng2, string fileName, int xSplit, int ySplit)
        {
            if (xSplit < 1) { xSplit = 1; }
            if (ySplit < 1) { ySplit = 1; }

            for(var i = 1; i <= xSplit; i++)
            {
                var lastLat = lat1 + (lat2 - lat1) / xSplit * (i - 1);
                var currentLat = lat1 + (lat2 - lat1) / xSplit * i;
                for (var j = 1; j <= ySplit; j++)
                {
                    var lastLng = lng1 + (lng2 - lng1) / ySplit * (j - 1);
                    var currentLng = lng1 + (lng2 - lng1) / ySplit * i;
                    await Download(query, lastLat, currentLat, lastLng, currentLng, fileName);
                }
            }
        }

        private async Task Download(string query, double? lastLat, double? currentLat, double? lastLng, double? currentLng, string fileName)
        {
            var client = BaiduClientCreator.Create();
            var index = 0;

            var req = BuildRequest(query, lastLat, lastLng, currentLat, currentLng, index);
            txbResult.AppendText($"正在下载 {index+1} 页，矩形区域：{lastLat},{lastLng}, -- {currentLat},{currentLng}  关键字: {query}" + Environment.NewLine);

            PlaceSearchResponse resp = await client.ExecuteAsync(req);
            System.Threading.Thread.Sleep(GlobalSetting.SLEEP);

            if (resp?.Total == GlobalSetting.MAX_TOTAL)
            {
                txbResult.AppendText($"矩形区域：{lastLat},{lastLng}, -- {currentLat},{currentLng}  关键字: {query} 结果>={GlobalSetting.MAX_TOTAL}条，进行区域拆分查询");
                await SplitRectange(query, lastLat, currentLat, lastLng, currentLng, fileName, 2, 2);
            }
            else if (resp.Status == 0 && resp?.Results?.Count == 0)
            {
                txbResult.AppendText($"第 {index + 1} 页共有 0 条数据" + Environment.NewLine);
            }
            else if(resp.Status == 0 && resp?.Results?.Count >0)
            {
                txbResult.AppendText($"本区域查询结果数量为{resp.Total}条" + Environment.NewLine);

                while (resp?.Results?.Count > 0)
                {
                    List<ExportModels.PlaceDetail> list = Mapper.Map<List<ExportModels.PlaceDetail>>(resp.Results);
                    txbResult.AppendText($"第 {index + 1} 页共有 {list?.Count ?? 0} 条数据" + Environment.NewLine);
                    CsvWriter.Write(fileName, list);
                    req = BuildRequest(query, lastLat, lastLng, currentLat, currentLng, ++index);
                    resp = await client.ExecuteAsync(req);
                    System.Threading.Thread.Sleep(GlobalSetting.SLEEP);
                }
            }

            if (resp.Status != 0)
            {
                txbResult.AppendText($"第 {index + 1} 页查询出错：" + resp.Meta);
            }
        }

        private PlaceSearchRequest<PlaceRectangeModel> BuildRequest(string query, double? lat1, double? lng1, double? lat2, double? lng2, int index)
        {
            var model = new PlaceRectangeModel()
            {
                Query = query,
                Bounds = $"{(lat1>lat2 ? lat2: lat1)},{(lng1>lng2 ? lng2: lng1)},{(lat2 < lat1 ? lat1 : lat2)},{(lng2 < lng1 ? lng1 : lng2)}",
                Page_Size = GlobalSetting.PAGE_SIZE,
                Page_Num = index
            };
            return new PlaceSearchRequest<PlaceRectangeModel>(model);
        }
    }
}
