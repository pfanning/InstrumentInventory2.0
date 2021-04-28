using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentInventory2._0
{
    class Instrument
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Other { get; set; }

        public override string ToString()
        {
            return $"{Model}";
        }           
        
    }        
}

