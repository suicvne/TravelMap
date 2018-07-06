using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;


namespace TravelMap
{
    public class TravelMapLocationDelegate : NSTableViewDelegate
    {
        private const string CellIdentifier = "LocationCell";

        private TravelMapLocationDataSource DataSource;

        public TravelMapLocationDelegate(TravelMapLocationDataSource dataSource)
        {
            DataSource = dataSource;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            // This pattern allows you reuse existing views when they are no-longer in use.
            // If the returned view is null, you instance up a new view
            // If a non-null view is returned, you modify it enough to reflect the new data
            NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            // Setup view based on the column selected
            switch (tableColumn.Title)
            {
                case "Date":
                    view.StringValue = DataSource.Locations[(int)row].Date;
                    break;
                case "Location":
                    view.StringValue = DataSource.Locations[(int)row].LocationNameString;
                    break;
            }

            return view;
        }
    }
}
