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
        static string? _caseNo;
        public string CaseNo { get; set; }
        public Patient Patient { get; set; }
        public Priority Priorty { get; set; }
        public EmergencyStatus Status { get; set; }
        public Ambulance? AssignedAmbulance { get; set; }

       

        public EmergencyCase(Patient patient, Priority priorty)
        {
            Patient = patient;
            Priorty = priorty;
            _caseNo = Helper.GenerateCaseNo();
            CaseNo = _caseNo;

        }

        public override string ToString()
        {
            return $"\n-------\nPatient:{Patient}\nPriorty:{Priorty}\nCaseNo:{CaseNo}\nStatus:{Status}\nAssignedAmbulance:{AssignedAmbulance}\n-------\n";
        }
       




    }

}
