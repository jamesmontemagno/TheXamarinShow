using System;
using Android.App;
using Android.Widget;
using Android.OS;

using System.Linq;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;

using CoffeeApp.Logic;

namespace CoffeeApp.Droid
{
    [Activity(Label = "Coffee App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {

        GoogleMap map;
        MapFragment mapFragment;

        Button addPin, loadCoffee;
        Handler handler;

        CoffeesViewModel viewModel;
 
         
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            viewModel = new CoffeesViewModel();

            handler = new Handler();

            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);

            addPin = FindViewById<Button>(Resource.Id.button_add_pin);
            loadCoffee = FindViewById<Button>(Resource.Id.button_load_coffee);

            addPin.Enabled = loadCoffee.Enabled = false;

            mapFragment.GetMapAsync(this);

            addPin.Click += AddPin_Click;

            loadCoffee.Click += LoadCoffee_Click;
            
        }

        private async void LoadCoffee_Click(object sender, EventArgs e)
        {
            map.Clear();


            var items = await viewModel.GetCoffees();
            foreach(var item in items)
            {
                var marker = new MarkerOptions()
                .SetPosition(new LatLng(item.Latitude, item.Longitude))
                .SetTitle(item.Name);
                map.AddMarker(marker);
            }

            
            
        }

        private async void AddPin_Click(object sender, EventArgs e)
        {
            var location = map.CameraPosition.Target;

            var title = await viewModel.GetNameForLocation(this, location.Latitude, location.Longitude);

            var marker = new MarkerOptions()
                .SetPosition(new LatLng(location.Latitude, location.Longitude))
                .SetTitle(title);
            map.AddMarker(marker);
            
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            if (googleMap == null)
                return;

            map = googleMap;
            addPin.Enabled = loadCoffee.Enabled = true;

            handler.PostDelayed(ZoomToSeattle, 1000);
        }

        void ZoomToSeattle()
        {
           
            var location = new LatLng(47.6062, -122.3321);
            var builder = CameraPosition.InvokeBuilder()
                .Target(location)
                .Zoom(14);

            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(builder.Build());
            RunOnUiThread(()=>map?.MoveCamera(cameraUpdate));
        }
    }
}

