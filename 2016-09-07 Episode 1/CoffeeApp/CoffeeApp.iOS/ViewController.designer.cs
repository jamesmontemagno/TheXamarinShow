// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace CoffeeApp
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MyButtonLoadData { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MyButtonPin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView MyMap { get; set; }

        [Action ("MyButtonPin_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MyButtonPin_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (MyButtonLoadData != null) {
                MyButtonLoadData.Dispose ();
                MyButtonLoadData = null;
            }

            if (MyButtonPin != null) {
                MyButtonPin.Dispose ();
                MyButtonPin = null;
            }

            if (MyMap != null) {
                MyMap.Dispose ();
                MyMap = null;
            }
        }
    }
}