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
using BaiduMap.Util;
using BaiduMap.Request;
using BaiduMap.Request.Models;
using Newtonsoft.Json;
using BaiduMap.Response.Place;
using AutoMapper;

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for RegioQuery.xaml
    /// </summary>
    public partial class RegionDownloadControl : UserControl
    {
        public RegionDownloadControl()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            btnDownload.IsEnabled = false;
            txbResult.Clear();

            var client = BaiduClientCreator.Create();
            var index = 0;
            PlaceSearchResponse resp = null;
            string region = txbRegion.Text.Trim();
            string query = txbKey.Text.Trim();
            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download", $"{region}_{query}.csv");
            do
            {
                var req = BuildRequest(region, query, index++);
                txbResult.AppendText($"正在下载第 {index} 页，查询区域：{region}, 关键字: {query}" + Environment.NewLine);
                resp = await client.ExecuteAsync(req);
                List<ExportModels.PlaceDetail> list = Mapper.Map<List<ExportModels.PlaceDetail>>(resp.Results);
                txbResult.AppendText($"第 {index} 页共有 {list?.Count ?? 0} 条数据" + Environment.NewLine);
                CsvWriter.Write(fileName, list);
                System.Threading.Thread.Sleep(GlobalSetting.SLEEP);
            } while (resp?.Results?.Count > GlobalSetting.PAGE_SIZE);

            btnDownload.IsEnabled = true;
        }

        private PlaceSearchRequest<PlaceRegionModel> BuildRequest(string region, string query, int index)
        {
            var model = new PlaceRegionModel()
            {
                Region = region,
                Query = query,
                Page_Num = index,
                Page_Size = GlobalSetting.PAGE_SIZE,
            };
            return new PlaceSearchRequest<PlaceRegionModel>(model);
        }
    }
}
