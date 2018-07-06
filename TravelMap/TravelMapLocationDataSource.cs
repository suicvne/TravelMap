using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;


namespace TravelMap
{
    public class TravelMapLocationDataSource : NSTableViewDataSource
    {
        public List<TravelMapLocation> Locations = new List<TravelMapLocation>();

        public TravelMapLocationDataSource()
        {
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return this.Locations.Count;
        }
    }
}
