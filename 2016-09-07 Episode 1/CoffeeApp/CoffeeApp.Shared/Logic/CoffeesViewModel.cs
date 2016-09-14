using System.Collections.Generic;
using System.Threading.Tasks;

using AppServiceHelpers;
using AppServiceHelpers.Abstractions;
using System.Linq;
#if __IOS__
using CoreLocation;
#elif __ANDROID__
using Android.Content;
using Android.Locations;
#endif

namespace CoffeeApp.Logic
{
    public class CoffeesViewModel
    {
        ITableDataStore<Coffee> table;
        
        public CoffeesViewModel()
        {

            // 1. Create a new EasyMobileServiceClient.
            var client = EasyMobileServiceClient.Create();

            // 2. Initialize the library with the URL of the Azure Mobile App you created in Step #1.

            client.Initialize("https://YOUR_URL_HERE.azurewebsites.net");

            // 3. Register a model with the EasyMobileServiceClient to create a table.
            client.RegisterTable<Coffee>();

            // 4. Finalize the schema for our database. All table registrations must be done before this step.
            client.FinalizeSchema();

            table = client.Table<Coffee>();
        }

        public Task<IEnumerable<Coffee>> GetCoffees() => table.GetItemsAsync();
        

        public Task<bool> AddCoffee(string name, double latitude, double longitude)
        {
            return table.AddAsync(new Coffee
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            });
        }

#if __IOS__
        public async Task<string> GetNameForLocation(double latitude, double longitude)
        {

            var coder = new CLGeocoder();

            var locations = await coder.ReverseGeocodeLocationAsync(new CLLocation
                (latitude, longitude));

            var name = locations?.FirstOrDefault()?.Name ?? "unknown";


            await AddCoffee(name, latitude, longitude);

            return name;
        }
#elif __ANDROID__
        public async Task<string> GetNameForLocation(Context context, double latitude, double longitude)
        {

            var coder = new Geocoder(context);
            var locations = await coder.GetFromLocationAsync(latitude, longitude, 1);

            var name = $"{locations?.FirstOrDefault()?.SubThoroughfare ?? string.Empty} " +
                            $"{locations?.FirstOrDefault()?.Thoroughfare ?? string.Empty}";

            await AddCoffee(name, latitude, longitude);

            return name;
        }
#endif

    }
}
