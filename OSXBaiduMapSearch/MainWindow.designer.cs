// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace OSXBaiduMapSearch
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		AppKit.NSButton btnCirum { get; set; }

		[Outlet]
		AppKit.NSButton btnGeo { get; set; }

		[Outlet]
		AppKit.NSButton btnRect { get; set; }

		[Outlet]
		AppKit.NSTextField centLat { get; set; }

		[Outlet]
		AppKit.NSTextField centLng { get; set; }

		[Outlet]
		AppKit.NSTextField centQueryKey { get; set; }

		[Outlet]
		AppKit.NSTextField centRadius { get; set; }

		[Outlet]
		AppKit.NSTextField centResult { get; set; }

		[Outlet]
		AppKit.NSTextField geoAddress { get; set; }

		[Outlet]
		AppKit.NSTextField geoCity { get; set; }

		[Outlet]
		AppKit.NSTextField geoResult { get; set; }

		[Outlet]
		AppKit.NSTextField leftLat { get; set; }

		[Outlet]
		AppKit.NSTextField leftLng { get; set; }

		[Outlet]
		AppKit.NSTextField rectQueryKey { get; set; }

		[Outlet]
		AppKit.NSTextField rectResult { get; set; }

		[Outlet]
		AppKit.NSTextField rightLat { get; set; }

		[Outlet]
		AppKit.NSTextField rightLng { get; set; }

		[Action ("btnCentQuery:")]
		partial void btnCentQuery (Foundation.NSObject sender);

		[Action ("btnGeoQuery:")]
		partial void btnGeoQuery (Foundation.NSObject sender);

		[Action ("btnRectQuery:")]
		partial void btnRectQuery (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnGeo != null) {
				btnGeo.Dispose ();
				btnGeo = null;
			}

			if (btnCirum != null) {
				btnCirum.Dispose ();
				btnCirum = null;
			}

			if (btnRect != null) {
				btnRect.Dispose ();
				btnRect = null;
			}

			if (centLat != null) {
				centLat.Dispose ();
				centLat = null;
			}

			if (centLng != null) {
				centLng.Dispose ();
				centLng = null;
			}

			if (centQueryKey != null) {
				centQueryKey.Dispose ();
				centQueryKey = null;
			}

			if (centRadius != null) {
				centRadius.Dispose ();
				centRadius = null;
			}

			if (centResult != null) {
				centResult.Dispose ();
				centResult = null;
			}

			if (geoAddress != null) {
				geoAddress.Dispose ();
				geoAddress = null;
			}

			if (geoCity != null) {
				geoCity.Dispose ();
				geoCity = null;
			}

			if (geoResult != null) {
				geoResult.Dispose ();
				geoResult = null;
			}

			if (leftLat != null) {
				leftLat.Dispose ();
				leftLat = null;
			}

			if (leftLng != null) {
				leftLng.Dispose ();
				leftLng = null;
			}

			if (rectQueryKey != null) {
				rectQueryKey.Dispose ();
				rectQueryKey = null;
			}

			if (rectResult != null) {
				rectResult.Dispose ();
				rectResult = null;
			}

			if (rightLat != null) {
				rightLat.Dispose ();
				rightLat = null;
			}

			if (rightLng != null) {
				rightLng.Dispose ();
				rightLng = null;
			}
		}
	}
}
