using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Ambulance_Dispatch_System.models
{
    public class Patient
    {
        private static int _idCounter=1;
        public int Id { get; private set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }


        public Patient(string FullName,string PhoneNumber,string Location)
        {
            Id = _idCounter++;
            this.FullName = FullName;
            this.PhoneNumber = PhoneNumber;
            this.Location = Location;
        
        }
    }
}
