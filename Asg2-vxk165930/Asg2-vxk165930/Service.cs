using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asg2_vxk165930
{
    class Service
    {
        /* Dictionary contains the key and value pairs. Here key is the primary key and value is the newRecord object*/

        Dictionary<String, RecordDetails> recordedData = new Dictionary<string, RecordDetails>();

        /*Data that is shown on the data grid*/

        List<string[]> allRows = new List<string[]>(); 
        
        public Service()
        {

        }
        
        /*Method to generate primary key. Primary key is a combination of first name, last name and phone number*/
        public String generatePrimaryKey(RecordDetails newRecord)
        {
            String primaryKey = newRecord.FirstName + " " + newRecord.LastName + " " + newRecord.PhoneNumber;
            return primaryKey;
        }

        /*This method adds new record if the combination of first name, last name and phone number is not present*/

        public Boolean addNewRecord(RecordDetails newRecord)
        {
            Boolean success = false;
            string primaryKey = this.generatePrimaryKey(newRecord);
            newRecord.PrimaryKey = primaryKey;
            if (!recordedData.ContainsKey(primaryKey))
            {
                recordedData.Add(primaryKey, newRecord);
                success = true;
            }
            return success;
        }
        
        /*This method deletes a record if it is present*/
        public Boolean deleteRecord(string primaryKey)
        {
            Boolean success = false;
            if (recordedData.ContainsKey(primaryKey))
            {
                recordedData.Remove(primaryKey);
                success = true;
            }
            return success;
        }
        
        /*This method modifes a record if it does not conflict with the already present records*/
        public Boolean modifyRecord(RecordDetails newRecord, string newPrimaryKey)
        {
            Boolean success = false;
            string primaryKey = this.generatePrimaryKey(newRecord);
            newRecord.PrimaryKey = primaryKey;
            if (newPrimaryKey.Equals(primaryKey))
            {
                recordedData.Remove(newPrimaryKey);
                recordedData.Add(primaryKey, newRecord);
                success = true;
            }
            else
            {
                if (!recordedData.ContainsKey(primaryKey))
                {
                    recordedData.Remove(newPrimaryKey);
                    recordedData.Add(primaryKey, newRecord);
                    success = true;
                }
                   
            }
            return success;
        }
       
        /*This method is to populate the datagrid with the values from the file*/
        public List<string[]> dataGridGenerate()
        {
            allRows = new List<String[]>();
            ICollection<String> primaryKeys = recordedData.Keys;
            foreach (var primaryKey in primaryKeys)
            {
                RecordDetails newRecord = recordedData[primaryKey];
                string[] newRow = new string[] { newRecord.FirstName + " " + newRecord.LastName, newRecord.PhoneNumber };
                allRows.Add(newRow);
            }
            return allRows;
        }

        /*This method is to get the data to the fields when the user wants to modify or delete*/
        public RecordDetails getNewRecord(int gridIndex)
        {
            string[] row = allRows[gridIndex];
            string fullName = row[0];
            string phoneNumber = row[1];
            string primaryKey = fullName + " " + phoneNumber;
            RecordDetails newRecord = null;
            if (recordedData.ContainsKey(primaryKey))
            {
                newRecord = recordedData[primaryKey];
            }
            return newRecord;
        }

        /*This method is to write the tab separated data in the file*/
        
        public void writeData()
        {
            ICollection<String> primaryKeys = recordedData.Keys;
            if (!System.IO.File.Exists(System.IO.Path.GetFullPath(@"..\..\CS6326Asg2.txt")))
            {
                return;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.Path.GetFullPath(@"..\..\CS6326Asg2.txt")))
            {
                foreach (var primaryKey in primaryKeys)
                {
                    RecordDetails newRecord = recordedData[primaryKey];
                    String newLine = newRecord.FirstName + "\t" + newRecord.LastName + "\t" + newRecord.MiddleInitial + "\t" +newRecord.AddressLine1 + "\t"+
                                     newRecord.AddressLine2 + "\t"+ newRecord.City + "\t" + newRecord.State + "\t" + newRecord.Zipcode + "\t" + newRecord.Gender + "\t"+ 
                                     newRecord.PhoneNumber + "\t" + newRecord.Email + "\t" + newRecord.ProofOfPurchase+"\t" + newRecord.DateReceived + "\t"+
                                     newRecord.StartTime + "\t" + newRecord.EndTime + "\t" + newRecord.BackSpaceCount;

                    file.WriteLine(newLine);
                }
            }
        }
        
        /*THis method is to read the data from the file to view and edit*/
         public void readData()
         {
             String newLine;
             if (!System.IO.File.Exists(System.IO.Path.GetFullPath(@"..\..\CS6326Asg2.txt")))
             {
                 return;
             }
             System.IO.StreamReader file = new System.IO.StreamReader(System.IO.Path.GetFullPath(@"..\..\CS6326Asg2.txt"));
             while ((newLine = file.ReadLine()) != null)
             {
                 String[] data = newLine.Split('\t');
                 int index = 0;
                 RecordDetails newRecord = new RecordDetails();
                 newRecord.FirstName = data[index++];
                 newRecord.LastName = data[index++];
                 newRecord.MiddleInitial = data[index++];
                 newRecord.AddressLine1 = data[index++];
                 newRecord.AddressLine2 = data[index++];
                 newRecord.City = data[index++];
                 newRecord.State = data[index++];
                 newRecord.Zipcode = data[index++];
                 newRecord.Gender = data[index++];
                 newRecord.PhoneNumber = data[index++];
                 newRecord.Email = data[index++];
                 newRecord.ProofOfPurchase = data[index++];
                 newRecord.DateReceived = data[index++];                
                 newRecord.StartTime = data[index++];
                 newRecord.EndTime = data[index++];
                 newRecord.BackSpaceCount = Convert.ToInt32(data[index++]);

                 String primaryKey = generatePrimaryKey(newRecord);
                 newRecord.PrimaryKey = primaryKey;
                 recordedData.Add(primaryKey, newRecord);
             }
             file.Close();
         }

    }
}
