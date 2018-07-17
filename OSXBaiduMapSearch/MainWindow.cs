using System;

using Foundation;
using AppKit;
using OSXBaiduMapSearch.Middle;
using System.Threading.Tasks;

namespace OSXBaiduMapSearch
{
    public partial class MainWindow : NSWindow
    {

        public MainWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public MainWindow(NSCoder coder) : base(coder)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }

        async partial void btnRectQuery(Foundation.NSObject sender)
        {
            btnRect.Enabled = false;
            var query = rectQueryKey.StringValue;
            var lastLat = leftLat.DoubleValue;
            var lastLng = leftLng.DoubleValue;
            var currentLat = rightLat.DoubleValue;
            var currentLng = rightLng.DoubleValue;

            await Task.Run(() => 
            {
                var helper = new RectangeHelper(query, lastLat, lastLng, currentLat, currentLng);
                var fileName = System.IO.Path.Combine(Setting.Instance.BaseDirectory, $"{lastLng}_{lastLat}_{query}_{currentLng}_{currentLat}.csv");
                helper.LongEventHandler += RectWriteLog;
                helper.Download(fileName);
            });

            btnRect.Enabled = true;
        }

        private void RectWriteLog(string message) 
        {
            this.BeginInvokeOnMainThread(() => rectResult.StringValue += message);
        }

        private void CirumWriteLog(string message)
        {
            this.BeginInvokeOnMainThread(() => centResult.StringValue += message);

        }

        async partial void btnGeoQuery(Foundation.NSObject sender)
        {
            btnGeo.Enabled = false;
            var address = geoAddress.StringValue;
            var city = geoCity.StringValue;
            var helper = new GeoCoderHelper(address, city);
            var result = await Task.Run(() => helper.Query());
            geoResult.StringValue = result;

            btnGeo.Enabled = true;
        }

        async partial void btnCentQuery(Foundation.NSObject sender)
        {
            btnCirum.Enabled = false;
            var query = centQueryKey.StringValue;
            var lat = centLat.DoubleValue;
            var lng = centLng.DoubleValue;
            var radius = centRadius.IntValue;
            await Task.Run(() =>
            {
                var helper = new CirumHelper(query, lat, lng, radius);
                var fileName = System.IO.Path.Combine(Setting.Instance.BaseDirectory, $"{lng}_{lat}_{query}_{radius}.csv");
                helper.LogEventHandler += CirumWriteLog;
                helper.Download(fileName);
            });

            btnCirum.Enabled = true;
        }
    }
}
