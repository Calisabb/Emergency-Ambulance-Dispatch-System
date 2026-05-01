using Emergency_Ambulance_Dispatch_System.Enums;
using Emergency_Ambulance_Dispatch_System.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Ambulance_Dispatch_System.Service_Layer
{
    internal class EmergencyService
    {
        int _ambulanceLimit;
        public int AmbulanceLimit 
        {
            get
            { 
                return _ambulanceLimit;
            
            }
            set
            {
                if (value < 0 || value > 5)
                {
                    
                    throw new ArgumentException("!!!limit must be between 0 and 5!!!");
                }
                else
                { 
                    _ambulanceLimit = value;
                }
            
            }
        }

        public EmergencyService(int limit)
        {
            AmbulanceLimit = limit;
        }

        public List<EmergencyCase> cases = new List<EmergencyCase>();
        public  List<Ambulance> ambulances = new List<Ambulance>();

        public void AddAmbulance(string plateNumber, string driverName)
        {
            if (ambulances.Count <= AmbulanceLimit)
            { 
                Ambulance ambulance = new Ambulance(plateNumber, driverName);
                ambulances.Add(ambulance);
            }
        
        }

        public void CreateEmergencyCase(Patient patient, Priority priority)
        { 
            EmergencyCase cas = new EmergencyCase(patient, priority);
            cas.Status = EmergencyStatus.Created;
            cases.Add( cas );
        }

        public void AssignAmbulance(string caseNo)
        {
            var cas = cases.FirstOrDefault(x => x.CaseNo==caseNo && x.Status == EmergencyStatus.Created);
            if (cas == null)
            {
                throw new Exception("Not found");
            }
            
            var ambulance = ambulances.FirstOrDefault(x => x.IsAvailable==true);
            if (ambulance is not null)
            {
                cas.Status = EmergencyStatus.Assigned;
                cas.AssignedAmbulance = ambulance;
                ambulance.IsAvailable = false;
                Console.WriteLine("Ambulance succesfully assigned");
            }
            else
            {
                Console.WriteLine("All ambulances are busy");
            }
  
        }
        public void StartDispatch(string caseNo)
        {
            var cas = cases.FirstOrDefault(x => x.CaseNo == caseNo && x.Status == EmergencyStatus.Assigned);
            if (cas == null)
            {
                Console.WriteLine("Not found");
            }
            else
            {
                cas.Status = EmergencyStatus.OnRoute;
                Console.WriteLine("Ambulance on road");
            }
        }
        public void CompleteCase(string caseNo)
        {
            var cas = cases.FirstOrDefault(x => x.CaseNo == caseNo && x.Status == EmergencyStatus.OnRoute);
            if (cas == null)
            {
                throw new Exception("Not found");
            }
            else
            {

                cas.Status = EmergencyStatus.Completed;
                var ambulance = cas.AssignedAmbulance;
                ambulance!.IsAvailable = true;
                cas.AssignedAmbulance = null;
                Console.WriteLine("Ambulance complete work");

            }
           

        }

        public EmergencyCase GetCase(string caseNo)
        {
            var curr = cases.FirstOrDefault(x => x.CaseNo == caseNo);
            if (curr == null)
            {
                throw new Exception();

            }
            else
            { 
                return curr;
            }
        }

        public List<EmergencyCase> GetAllCases()
        {
            return cases;
        
        }
        public List<EmergencyCase> GetCasesByStatus(EmergencyStatus status)
        {
            return cases.FindAll(x=>x.Status == status );
            
        }
        public List<EmergencyCase> GetHighPriorityCases(Priority priority)
        {
            return cases.FindAll(x=>x.Priorty==priority);
            
        }
        public List<Ambulance> GetAvailableAmbulances()
        {
            return ambulances.FindAll(x=> x.IsAvailable == true);
        }

        public void SystemInfo()
        {
            var totalCount = cases.Count;

            var active = cases.FindAll(x=> x.Status != EmergencyStatus.Completed);
            var activeCount = active.Count;

            var completed = cases.FindAll(x => x.Status == EmergencyStatus.Completed);
            var completedCount = completed.Count;

            var availableAmbulance = ambulances.FindAll(x => x.IsAvailable==true);
            var availableAmbulanceCount = availableAmbulance.Count; 

            Console.WriteLine($"Total cases: {totalCount}\nActive cases: {activeCount}\nCompleted cases: {completedCount}\nAvailable ambulances: {availableAmbulanceCount}");
        }

      


    }
}
