﻿using System;
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
using BaiduMap.Request;
using BaiduMap.Request.Models;
using Newtonsoft.Json;

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for DetailQueryControl.xaml
    /// </summary>
    public partial class DetailQueryControl : UserControl
    {
        public DetailQueryControl()
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

        private PlaceDetailRequest BuildRequest()
        {
            var uid = txbKey.Text.Trim();
            var model = new PlaceDetailModel()
            {
                Uid = uid
            };
            return new PlaceDetailRequest(model);
        }
    }
}
