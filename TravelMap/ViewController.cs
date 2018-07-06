using System;

using AppKit;
using Foundation;
using MapKit;

namespace TravelMap
{
    public partial class ViewController : NSViewController
    {
        private TravelMapLocationDataSource LocationsDataSource = new TravelMapLocationDataSource();
        private TravelMapLocationDelegate LocationsDelegate;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            // Populate the Product Table
            LocationTableView.DataSource = LocationsDataSource;
            LocationsDelegate = new TravelMapLocationDelegate(LocationsDataSource);
            LocationTableView.Delegate = LocationsDelegate;
        }

        partial void locationCenterClick(NSObject sender)
        {
            if (mainMapView.UserLocationVisible)
            {
                var curUserLocation = mainMapView.UserLocation;

                MKCoordinateRegion region = new MKCoordinateRegion();
                region.Center.Latitude = (curUserLocation.Location.Coordinate.Latitude);
                region.Center.Longitude = (curUserLocation.Location.Coordinate.Longitude);
                region.Span.LatitudeDelta = 1;
                region.Span.LongitudeDelta = 1;
                region = mainMapView.RegionThatFits(region);

                mainMapView.SetRegion(region, animated: true); //zooms
                //mainMapView.SetCenterCoordinate(curUserLocation.Coordinate, animated: true); //doesn't zoom

                LocationsDataSource.Locations.Add(new TravelMapLocation(DateTime.Now.ToString(), 
                                                                        curUserLocation.Coordinate));
                LocationTableView.ReloadData();

            }
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
