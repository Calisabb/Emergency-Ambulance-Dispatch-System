using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency_Ambulance_Dispatch_System.Helpers
{
    public class Helper
    {
        public static bool CheckFullName(string fullname)
        {
            return !string.IsNullOrWhiteSpace(fullname);
        }


        public static bool CheckPhoneNumber(string phone)
        {
            if (phone.Length >= 10 && phone.Length <= 15)
            {
                return true;
            }
            else
            { 
                return false;
            }

        }

        public static bool CheckLocation(string location)
        {
            return !string.IsNullOrWhiteSpace(location);
        }


        public static string GenerateCaseNo()
        {
            Random rnd = new Random();
            string caseNo = "EMG";
            for (int i = 0; i < 10; i++)
            {
                caseNo += rnd.Next(9);
            }
            return caseNo;
        }



    }
}
