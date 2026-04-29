using Emergency_Ambulance_Dispatch_System.Enums;
using Emergency_Ambulance_Dispatch_System.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Ambulance_Dispatch_System.models
{
    public class EmergencyCase
    {
        private string CaseN0 { get; set; }
        public string Patient { get; set; }
        public string Priorty { get; set; }
        public string Status { get; private set; }
        public string AssignedAmbulance { get; set; }

       

        public EmergencyCase(string patient, string priorty,string status)
        {
            Patient = patient;
            Priorty = priorty;
            Status = status;
            CaseN0 = Helper.GenerateCaseNo();

        }
       
        public bool AssignAmbulance(Ambulance ambulance)
        {
            if (ambulance == null)
            { return false; }


            else
            { return true; }

        }



    }

}
