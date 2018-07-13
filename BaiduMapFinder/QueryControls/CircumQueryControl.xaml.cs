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

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for CircumQueryControl.xaml
    /// </summary>
    public partial class CircumQueryControl : UserControl
    {
        public CircumQueryControl()
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

        private PlaceSearchRequest<PlaceCircumModel> BuildRequest()
        {
            var lat1 = location1.Location.Lat;
            var lng1 = location1.Location.Lng;
            var query = txbKey.Text.Trim();
            var radiusString = txbRadius.Text.Trim();
            int radius;
            int.TryParse(radiusString, out radius);

            var model = new PlaceCircumModel()
            {
                Query = query,
                Location = $"{lat1},{lng1}",
                Radius = radius > 0 ? radius.ToString() : "1000",
                Page_Size = 20
            };

            return new PlaceSearchRequest<PlaceCircumModel>(model);
        }
    }
}
