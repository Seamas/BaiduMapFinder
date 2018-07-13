using BaiduMap.Request;
using BaiduMap.Request.Models;
using Newtonsoft.Json;
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
using AutoMapper;
using BaiduMap.Response.Place;

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for CircumQueryControl.xaml
    /// </summary>
    public partial class CircumDownloadControl : UserControl
    {
        public CircumDownloadControl()
        {
            InitializeComponent();
        }

        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            btnQuery.IsEnabled = false;
            txbResult.Clear();

            var client = BaiduClientCreator.Create();
            var lat1 = location1.Location.Lat;
            var lng1 = location1.Location.Lng;
            var query = txbKey.Text.Trim();
            var radiusString = txbRadius.Text.Trim();
            int.TryParse(radiusString, out int radius);

            var index = 0;
            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download", $"{lng1}_{lat1}_{query}_{radius}.csv");
            var req = BuildRequest(query, lat1, lng1, radius, index);
            txbResult.AppendText($"正在下载第 {index + 1} 页，圆心：{lat1},{lng1}, 半径: {radius}  关键字: {query}" + Environment.NewLine);
            PlaceSearchResponse resp = await client.ExecuteAsync(req);
            System.Threading.Thread.Sleep(GlobalSetting.SLEEP);

            if (resp.Status == 0 && resp?.Results?.Count == 0)
            {
                txbResult.AppendText($"第 {index + 1} 页有 0 条数据" + Environment.NewLine);
            }
            else if (resp.Status == 0 && resp?.Results?.Count > 0)
            {
                if (resp?.Total == GlobalSetting.MAX_TOTAL)
                {
                    txbResult.AppendText($"本次查询结果数量为{resp.Total}条，百度地图最大限制为 {GlobalSetting.MAX_TOTAL} 条, 此次查询数据可能不全" + Environment.NewLine);
                }

                while (resp?.Results?.Count > 0)
                {
                    List<ExportModels.PlaceDetail> list = Mapper.Map<List<ExportModels.PlaceDetail>>(resp.Results);
                    txbResult.AppendText($"第 {index + 1} 页有 {list?.Count ?? 0} 条数据" + Environment.NewLine);
                    CsvWriter.Write(fileName, list);
                    req = BuildRequest(query, lat1, lng1, radius, ++index);
                    resp = await client.ExecuteAsync(req);
                    System.Threading.Thread.Sleep(GlobalSetting.SLEEP);
                }
            }
            
            if (resp?.Status != 0)
            {
                txbResult.AppendText($"查询第{index + 1}出错：" + resp?.Meta + Environment.NewLine);
            }

            btnQuery.IsEnabled = true;
        }

        private PlaceSearchRequest<PlaceCircumModel> BuildRequest(string query, double? lat, double? lng, int radius, int index)
        {
            var model = new PlaceCircumModel()
            {
                Query = query,
                Location = $"{lat},{lng}",
                Radius = radius > 0 ? radius.ToString() : "1000",
                Page_Num = index,
                Page_Size = GlobalSetting.PAGE_SIZE
            };

            return new PlaceSearchRequest<PlaceCircumModel>(model);
        }
    }
}
