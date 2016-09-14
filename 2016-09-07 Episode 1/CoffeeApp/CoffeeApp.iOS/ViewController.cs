using System;

using UIKit;
using System.Linq;
using MapKit;
using CoreLocation;
using CoffeeApp.Logic;

namespace CoffeeApp
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        CoffeesViewModel viewModel;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

           
            viewModel = new CoffeesViewModel();

            MyButtonLoadData.TouchUpInside += async (sender, args) =>
            {
                MyButtonLoadData.Enabled = false;
                MyMap.RemoveAnnotations(MyMap.Annotations);

                var coffees = await viewModel.GetCoffees();

                foreach (var coffee in coffees)
                {
                    MyMap.AddAnnotation(new MKPointAnnotation
                    {
                        Coordinate = new CLLocationCoordinate2D(coffee.Latitude, coffee.Longitude),
                        Title = coffee.Name
                    });
                }

                MyButtonLoadData.Enabled = true;

            };


            ZoomToSeattle();
         }

        void ZoomToSeattle()
        {

            var coords = new CLLocationCoordinate2D(47.6062, -122.3321);
            var span = new MKCoordinateSpan(DistanceUtils.MilesToLatitudeDegrees(20),
                DistanceUtils.MilesToLongitudeDegrees(20, coords.Latitude));

            MyMap.Region = new MKCoordinateRegion(coords, span);
        }

       
        async partial void MyButtonPin_TouchUpInside(UIButton sender)
        {
            var center = MyMap.Region.Center;

            var title = await viewModel.GetNameForLocation(center.Latitude, center.Longitude);

            MyMap.AddAnnotation(new MKPointAnnotation
            {
                Coordinate = center,
                Title = title
            });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}