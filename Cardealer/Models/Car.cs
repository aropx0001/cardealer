using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cardealer.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public CarBrand Brand { get; set; }
        public bool InStock { get; set; }
    }

}
