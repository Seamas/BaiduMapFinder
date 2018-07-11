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

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for RectangeQuery.xaml
    /// </summary>
    public partial class RectangeQueryControl : UserControl
    {
        public RectangeQueryControl()
        {
            InitializeComponent();
        }

        private async void btnQueryString_Click(object sender, RoutedEventArgs e)
        {
            var req = BuildRequest();
            var client = BaiduClientCreator.Create();
            var resp = await client.ExecuteReadStringAsync(req);
            txbResult.Text = resp;
        }

        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            var req = BuildRequest();
            var client = BaiduClientCreator.Create();
            var resp = await client.ExecuteAsync(req);
            txbResult.Text = JsonConvert.SerializeObject(resp);
        }

        private PlaceSearchRequest<PlaceRectangeModel> BuildRequest()
        {
            var lat1 = location1.Location.Lat;
            var lng1 = location1.Location.Lng;
            var lat2 = location2.Location.Lat;
            var lng2 = location2.Location.Lng;
            var query = txbKey.Text.Trim();

            var model = new PlaceRectangeModel()
            {
                Query = query,
                Bounds = $"{lat1},{lng1},{lat2},{lng2}",
                Page_Num =20
            };

            return new PlaceSearchRequest<PlaceRectangeModel>(model);
        }
    }
}
