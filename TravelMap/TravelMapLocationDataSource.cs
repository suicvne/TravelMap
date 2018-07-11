using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using CoreLocation;

namespace TravelMap
{
    // TODO: Serialize this class for loading and whatnot
    public class TravelMapLocationDataSource : NSTableViewDataSource
    {
        public event EventHandler RaiseLocationNameAcquired;
        public delegate void LocationNameAcquired(object sender, EventArgs eventArgs);

        public List<TravelMapLocation> Locations = new List<TravelMapLocation>();
        private Timer locationCheckTimer;

        public TravelMapLocationDataSource()
        {
            locationCheckTimer = new Timer(3000);

            locationCheckTimer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                Console.WriteLine($"Tick, updating {Locations.Count} items.");
                foreach(var locale in Locations)
                {
                    CLGeocoder coder = new CLGeocoder();
                    CLLocation clLocation = new CLLocation(locale.Location.Latitude, locale.Location.Longitude);

                    locale.LocationNameString = $"{locale.Location.Latitude}, {locale.Location.Longitude}";
                    coder.ReverseGeocodeLocation(clLocation, (placemarks, error) =>
                    {
                        CLPlacemark placemark = placemarks[0];
                        if (placemark != null)
                        {
                            locale.LocationNameString = placemark.Name;
                        }
                        if (error != null)
                        {
                            Console.WriteLine("Error Placemarking: " + error.LocalizedDescription);
                            locale.LocationNameString = $"{locale.Location.Latitude}, {locale.Location.Longitude}.";
                        }
                    });


                    RaiseLocationNameAcquired(this, new EventArgs());
                }
            };

            locationCheckTimer.Start();
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return this.Locations.Count;
        }
    }
}
