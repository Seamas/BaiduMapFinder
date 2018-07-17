using System;

using Foundation;
using AppKit;

namespace OSXBaiduMapSearch
{
    public partial class PreferenceWindow : NSWindow
    {
        public PreferenceWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public PreferenceWindow(NSCoder coder) : base(coder)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
