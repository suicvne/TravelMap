using System;

using AppKit;
using CoreLocation;
using Foundation;
using MapKit;

namespace TravelMap
{
    public partial class ViewController : NSViewController
    {
        private TravelMapLocationDataSource LocationsDataSource = new TravelMapLocationDataSource();
        private TravelMapLocationDelegate LocationsDelegate;

        private CLLocationManager locationManager;

        public ViewController(IntPtr handle) : base(handle)
        {
            locationManager = new CLLocationManager();
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

            (LocationTableView.DataSource as TravelMapLocationDataSource).RaiseLocationNameAcquired += (sender, e) =>
            {
                LocationTableView.ReloadData();
            };
        }

        partial void locationCenterClick(NSObject sender)
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                Console.WriteLine(CLLocationManager.Status);
                if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedAlways || CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
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
                    else
                    {
                        //TODO: Dialog
                        NSAlert alert = new NSAlert();
                        alert.MessageText = "Please Allow this App to Access your Location.";
                        alert.InformativeText = "If you accidentally denied this app access to your location, it can be enabled in System Preferences -> Security & Privacy -> Privacy -> Location Services.";
                        alert.RunSheetModal(this.View.Window);
                    }
                }
                else
                {
                    //TODO: Dialog
                    NSAlert alert = new NSAlert();
                    alert.MessageText = "Please Enable Location Services in macOS System Preferences";
                    alert.InformativeText = "Location Services can be enabled in System Preferences -> Security & Privacy->Privacy->Location Services.";
                    alert.RunSheetModal(this.View.Window);
                }
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
