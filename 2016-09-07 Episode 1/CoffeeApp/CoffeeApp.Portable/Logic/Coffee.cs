using AppServiceHelpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeApp.Logic
{
    public class Coffee : EntityData
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
