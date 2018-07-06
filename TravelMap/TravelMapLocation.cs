using System;
using MapKit;
using CoreLocation;

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


            CLGeocoder coder = new CLGeocoder();
            CLLocation clLocation = new CLLocation(Location.Latitude, Location.Longitude);

            LocationNameString = $"{location.Latitude}, {location.Longitude}";
            coder.ReverseGeocodeLocation(clLocation, (placemarks, error) =>
            {
                CLPlacemark placemark = placemarks[0];
                if(placemark != null)
                {
                    LocationNameString = placemark.Name;
                }
                if(error != null)
                {
                    Console.WriteLine("Error Placemarking: " + error.LocalizedDescription);
                    LocationNameString = $"{location.Latitude}, {location.Longitude}";
                }
            });
        }
    }
}
