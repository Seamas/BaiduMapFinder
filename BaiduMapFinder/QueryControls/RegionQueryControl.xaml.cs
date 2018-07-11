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

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for RegioQuery.xaml
    /// </summary>
    public partial class RegionQueryControl : UserControl
    {
        public RegionQueryControl()
        {
            InitializeComponent();
        }

        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            var req = BuildRequest();
            var client = BaiduClientCreator.Create();
            var resp = await client.ExecuteAsync(req);
            txbResult.Text = JsonConvert.SerializeObject(resp);
        }

        private async void btnQueryString_Click(object sender, RoutedEventArgs e)
        {
            var req = BuildRequest();
            var client = BaiduClientCreator.Create();
            var resp = await client.ExecuteReadStringAsync(req);
            txbResult.Text = resp;
        }

        private PlaceSearchRequest<PlaceRegionModel> BuildRequest()
        {
            string region = txbRegion.Text.Trim();
            string query = txbKey.Text.Trim();

            var model = new PlaceRegionModel()
            {
                Region = region,
                Query = query,
                Page_Size = 20,
            };
            return new PlaceSearchRequest<PlaceRegionModel>(model);
        }
    }
}
