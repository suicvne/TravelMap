using System;
using MapKit;
using CoreLocation;
using System.Timers;

namespace TravelMap
{
    /**
     * 
     * The actual class that holds the data. 
     * 
     */ 

    public class TravelMapLocation
    {
        public string Date { get; set; } = "";
        public CLLocationCoordinate2D Location { get; set; }
        public string LocationNameString { get; set; }

        public TravelMapLocation()
        {
        }

        public TravelMapLocation(string date, CLLocationCoordinate2D location)
        {
            Date = date;
            Location = location;
        }
    }
}
