using AppKit;
using Foundation;

namespace OSXBaiduMapSearch
{
    [Register("AppDelegate")]
    public partial class AppDelegate : NSApplicationDelegate
    {
        private MainWindowController mainWindowController;
        private PreferenceWindowController preferenceWindowController;

        public AppDelegate()
        {
            preferenceWindowController = new PreferenceWindowController();
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application
            mainWindowController = new MainWindowController();
            mainWindowController.Window.MakeKeyAndOrderFront(this);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }

        partial void preferences(Foundation.NSObject sender)
        {
            preferenceWindowController.ShowWindow(this);
            preferenceWindowController.Window.MakeKeyAndOrderFront(this);
        }
    }
}
