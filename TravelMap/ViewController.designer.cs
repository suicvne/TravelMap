// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace TravelMap
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSTableColumn DateColumn { get; set; }

        [Outlet]
        AppKit.NSTableColumn LocationColumn { get; set; }

        [Outlet]
        AppKit.NSTableView LocationTableView { get; set; }

        [Outlet]
        MapKit.MKMapView mainMapView { get; set; }

        [Action ("locationCenterClick:")]
        partial void locationCenterClick (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (LocationTableView != null) {
                LocationTableView.Dispose ();
                LocationTableView = null;
            }

            if (DateColumn != null) {
                DateColumn.Dispose ();
                DateColumn = null;
            }

            if (LocationColumn != null) {
                LocationColumn.Dispose ();
                LocationColumn = null;
            }

            if (mainMapView != null) {
                mainMapView.Dispose ();
                mainMapView = null;
            }
        }
    }
}
