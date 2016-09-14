using System.Collections.Generic;
using System.Threading.Tasks;

using AppServiceHelpers;
using AppServiceHelpers.Abstractions;

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

            client.Initialize("https://motzcoffee.azurewebsites.net");

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

        public Task<string> GetNameForLocation(double latitude, double longitude)
        {
            
            return Task.FromResult("unknown");
        }
        
    }
}
