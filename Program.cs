using Emergency_Ambulance_Dispatch_System.Enums;
using Emergency_Ambulance_Dispatch_System.Helpers;
using Emergency_Ambulance_Dispatch_System.models;
using Emergency_Ambulance_Dispatch_System.Service_Layer;

namespace Emergency_Ambulance_Dispatch_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmergencyService service = null!;
        Start:
            Console.WriteLine("Enter ambulance limit");
            if (!int.TryParse(Console.ReadLine(), out int limit))
            {
                Console.WriteLine("!!!Limit must be a number!!!");
                goto Start;
            }
            try
            {
                service = new EmergencyService(limit);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                goto Start;
            }

            do
            {
                Console.WriteLine("***Menu***\n1.Add Ambulance\n2.Create emergency case\n3.Assign ambulance\n" +
                    "4.Start dispatch\n5.Complete case\n6.Get case by caseNo\n7.Get all cases\n8.Filter by status\n9.High priority cases\n" +
                    "10.Available ambulances\n11.System Info\n0.Exit");
                var choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                    Plate:
                        Console.WriteLine("Enter plate number");
                        var plate = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(plate))
                        {
                            Console.WriteLine("Enter a valid plate number");
                            goto Plate;
                        }
                    NameA:
                        Console.WriteLine("Enter driver name");
                        var name = Console.ReadLine();
                        if (Helper.CheckFullName(name!) == false || name!.Any(char.IsDigit))
                        {
                            Console.WriteLine("Please enter a valid name");
                            goto NameA;

                        }
                        service.AddAmbulance(plate, name!);
                        break;
                    case "2":
                        Console.WriteLine("Enter patient's info");
                    Name:
                        Console.WriteLine("Name:");
                        var nameP = Console.ReadLine();
                        
                        if (Helper.CheckFullName(nameP!)==false || nameP!.Any(char.IsDigit))
                        {
                            Console.WriteLine("Please enter a valid name");
                            goto Name;
                        
                        }
                    Phone:
                        Console.WriteLine("Phone number:");
                        var phone = Console.ReadLine();
                        if (Helper.CheckPhoneNumber(phone!)==false || string.IsNullOrWhiteSpace(phone))
                        {
                            Console.WriteLine("Please enter a valid phone number");
                            goto Phone;
                        }
                    Location:
                        Console.WriteLine("Location:");
                        var location = Console.ReadLine();
                        if (Helper.CheckLocation(location!) || string.IsNullOrWhiteSpace(location))
                        {
                            Console.WriteLine("Please enter a valid location");
                            goto Location;
                        
                        }
                        Patient patient = new Patient(nameP!, phone, location);
                        Console.WriteLine("Choose priority\n1.Low\n2.Medium\n3.High");
                        var priorityChoose = Convert.ToInt32(Console.ReadLine());

                        switch (priorityChoose)
                        {
                            case 1:
                                var priorityL = Priority.Low;
                                service.CreateEmergencyCase(patient, priorityL);
                                break;
                            case 2:
                                var priorityM = Priority.Medium;
                                service.CreateEmergencyCase(patient, priorityM);
                                break;
                            case 3:
                                var priorityH = Priority.High;
                                service.CreateEmergencyCase(patient, priorityH);
                                break;
                        }
                        break;
                    case "3":
                        var data = service.cases;
                        var high = data.FindAll(x => x.Priorty == Enums.Priority.High && x.Status == Enums.EmergencyStatus.Created);
                        var medium = data.FindAll(x => x.Priorty == Enums.Priority.Medium && x.Status == Enums.EmergencyStatus.Created);
                        var low = data.FindAll(x => x.Priorty == Enums.Priority.Low && x.Status == Enums.EmergencyStatus.Created);

                        if (high.Count > 0)
                        {
                            foreach (var item in high)
                            {
                               service.AssignAmbulance(item.CaseNo);
                  
                            }
                        }
                        if (medium.Count > 0)
                        {
                            foreach (var item in medium)
                            {
                     
                                service.AssignAmbulance(item.CaseNo);   
                   
                            }
            
                        }
                        if (low.Count > 0)
                        {
                            foreach (var item in low)
                            {

                                service.AssignAmbulance(item.CaseNo);

                            }
                        }                  
                        break;
                    case "4":
                        var assignedCases = service.cases.FindAll(x => x.Status == EmergencyStatus.Assigned);
                        foreach (var c in assignedCases)
                        {
                            service.StartDispatch(c.CaseNo);

                        }
                        break;
                    case "5":
                        var onRouteCases = service.cases.FindAll(x => x.Status == EmergencyStatus.OnRoute);
                        foreach (var c in onRouteCases)
                        {
                            service.CompleteCase(c.CaseNo);

                        }
                        break;
                    case "6":
                        Console.WriteLine("Choose caseNo for info");
                        var cases = service.cases;
                        var ch = 1;
                        var dictionary = new Dictionary<int, string>();
                        foreach (var c in cases)
                        {
                            Console.WriteLine($"{ch}. {c.CaseNo}");
                            dictionary.Add(ch, c.CaseNo);
                            ch++;
                        }
                        var chooseCaseNo = Convert.ToInt32(Console.ReadLine());
                        var keys = dictionary.Keys;
                        foreach (var key in keys)
                        {
                            if (chooseCaseNo == key)
                            {
                                string value = dictionary[chooseCaseNo];
                                Console.WriteLine(service.GetCase(value)); 

                            }
                        }
                        break;
                    case "7":
                        var AllCases = service.cases;
                        foreach (var c in AllCases)
                        {
                            Console.WriteLine(c);
                        
                        }

                        break;
                    case "8":
                        Console.WriteLine("Choose emergency status\n1.Created\n2.Assigned\n3.OnRoute\n4.Completed");
                        var chooseEmergencyStatus = Convert.ToInt32(Console.ReadLine());
                        var casesByStatus = service.cases.FindAll(x => (int)x.Status == chooseEmergencyStatus);
                        foreach (var c in casesByStatus)
                        {
                            Console.WriteLine(c);
                        }
                        
                        break;
                    case "9":
                        var highPriorityCases = service.cases.FindAll(x => x.Priorty == Priority.High);
                        foreach (var c in highPriorityCases)
                        { 
                            Console.WriteLine(c); 
                        }
                        break;
                    case "10":
                        var ambulances = service.ambulances.FindAll(x => x.IsAvailable == true);
                        foreach (var a in ambulances)
                        { 
                            Console.WriteLine(a);
                        }
                        break;
                    case "11":
                        service.SystemInfo();
                        break;
                    case "0":
                        return;
                }
            } while (true);


        }
    }
}
