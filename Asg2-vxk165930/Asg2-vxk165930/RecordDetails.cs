using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_vxk165930
{

    /*This calss contains all the details that have to be saved per record*/
    class RecordDetails
    {
        private string firstName;
        private string lastName;
        private string middleInitial;
        private string addressLine1;
        private string addressLine2;
        private string city;
        private string state;
        private string zipcode;
        private string gender;
        private string phoneNumber;
        private string email;
        private string proofOfPurchase;
        private string dateReceived;
        private string startTime;
        private string endTime;
        private int backSpaceCount;
        private string primaryKey;

        public RecordDetails()
        {

        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string MiddleInitial { get => middleInitial; set => middleInitial = value; }
        public string AddressLine1 { get => addressLine1; set => addressLine1 = value; }
        public string AddressLine2 { get => addressLine2; set => addressLine2 = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zipcode { get => zipcode; set => zipcode = value; }
        public string Gender { get => gender; set => gender = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string ProofOfPurchase { get => proofOfPurchase; set => proofOfPurchase = value; }
        public string DateReceived { get => dateReceived; set => dateReceived = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }
        public int BackSpaceCount { get => backSpaceCount; set => backSpaceCount = value; }
        public string PrimaryKey { get => primaryKey; set => primaryKey = value; }
       
    }
}
