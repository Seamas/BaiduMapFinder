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

namespace BaiduMapFinder
{
    /// <summary>
    /// Interaction logic for LocationControl.xaml
    /// </summary>
    public partial class LocationControl : UserControl
    {
        
        public LocationModel Location { get; private set; } = new LocationModel();
        public LocationControl()
        {
            InitializeComponent();
            this.DataContext = Location;
        }
       

        public class LocationModel
        {
            public float? Lat { get; set; }
            public float? Lng { get; set; }
        }
    }
}
