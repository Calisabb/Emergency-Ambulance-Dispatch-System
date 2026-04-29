using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Ambulance_Dispatch_System.models
{
    public class Ambulance
    {
        private static int _idCounter=1;
        public int Id { get; private set; }
        public string PlateNumber { get; set; }
        public string DriverName { get; set; }
        public bool IsAvialable { get; set; } = true;


        public Ambulance(string PlateNumber,string DriverName)
        {
            Id = _idCounter++;
            this.PlateNumber = PlateNumber;
            this.DriverName = DriverName;
        
        }

    }
}
