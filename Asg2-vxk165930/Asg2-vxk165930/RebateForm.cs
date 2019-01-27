using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Asg2_vxk165930
{
    public partial class RebateForm : Form
    {

        Service service;
        private String newPrimaryKey;
        private String firstName;
        private String lastName;
        private String middleInitial;
        private String addressLine1;
        private String addressLine2;
        private String cityName;
        private String stateName;
        private String zipcode;
        private String genderVal;
        private String phoneNumber;
        private String emailAdd;
        private String proofOfPurchase;
        private String dateReceived;
        private String startTime;
        private String endTime;
        private String backSpaceCount;

        
        public RebateForm()
        {
            InitializeComponent();
            service = new Service();

        }

        /*When the form is loaded the data is read from the file. And the data grid is populated and also buttons are either enable
         or disabled based on the requirement. The instructios for the user are also displayed.*/
        private void RebateForm_Load(object sender, EventArgs e)
        {
            service.readData();
            this.dateReceivedToCurrentDate();
            this.updateGrid();
            addBtn.Enabled = true;
            modifyBtn.Enabled = false;
            delBtn.Enabled = false;
            resetBtn.Enabled = false;
            gridUsageBox.Text = "Click on the row header or any cell of a row \nfor which you want to modify or delete the record";
            btnUsageBox.Text = "ADD - Add a new Record     MODIFY - Modify the selected Record\n" +
                "RESET - Reset the form      DELETE - Delete the selected Record ";
            mandatoryFields.Text = "Fields marked with * are mandatory";
        }

        /* This method is invoked when the user presses the ADD button*/
        private void addBtn_Click(object sender, EventArgs e)
        {

            Boolean success = validateData();
            
            if (success)
            {

                Boolean success1 = saveData(success);
                if (success1)
                {
                    resetForm();
                    modifyBtn.Enabled = false;
                    delBtn.Enabled = false;
                    addBtn.Enabled = true;
                    resetBtn.Enabled = false;
                    updateGrid();
                    mandatoryFields.Text = "Sucessfully added the record";

                }
                else
                {
                    mandatoryFields.Text = "A record with same name and phone number \n already present";
                }

            }
           

        }

        /* This method is invoked when the user presses the MODIFY button*/
        private void modifyBtn_Click(object sender, EventArgs e)
        {
            Boolean success = validateData();
           
            if (success)
            {
                Boolean success1 = saveData(success);
                if (success1)
                {
                    resetForm();
                    modifyBtn.Enabled = false;
                    delBtn.Enabled = false;
                    addBtn.Enabled = true;
                    resetBtn.Enabled = false;
                    updateGrid();
                    mandatoryFields.Text = "Sucessfully updated the record";
                    service.writeData();
                }
                else
                {
                    mandatoryFields.Text = "A record with same name and phone number \n already present";
                }

            }
        }

        /* This method is invoked when the user presses the DELETE button*/
        private void delBtn_Click(object sender, EventArgs e)
        {

            String newPrimaryKey = txtPrimaryKey.Text;
            Boolean success = service.deleteRecord(newPrimaryKey);

            if (success || (newPrimaryKey == ""))
            {
                resetForm();
                modifyBtn.Enabled = false;
                delBtn.Enabled = false;
                addBtn.Enabled = true;
                resetBtn.Enabled = false;
                updateGrid();
                mandatoryFields.Text = "Sucessfully deleted the record";
                service.writeData();
            }

          

        }

        /*This method is to validate all the user input. THe max lenghts for every field is already defined using the Max Length property. 
         * But the same is also checked here*/
        private Boolean validateData()
        {
            Boolean success=true;
            long phone,zip,validDay;
            newPrimaryKey = txtPrimaryKey.Text.Trim();
            firstName = txtFirstName.Text.Trim();
            lastName = txtLastName.Text.Trim();
            middleInitial = txtMiddleName.Text.Trim();
            addressLine1 = txtAddLine1.Text.Trim();
            addressLine2 = txtAddLine2.Text.Trim();
            cityName = txtCity.Text.Trim();
            stateName = txtState.Text.Trim();
            zipcode = txtZip.Text.Trim();
            genderVal = comGender.Text.Trim();
            phoneNumber = txtPhone.Text.Trim();
            emailAdd = txtEmail.Text.Trim();
            proofOfPurchase = comProof.Text.Trim();
            dateReceived = txtDate.Text.Trim();
           
            if (firstName == "" || firstName.Length > 20)
            {
                txtFirstName.Focus();
                success = false;
                mandatoryFields.Text="Please enter first name (Max 20 characters)";
                return success;
            }
            if (lastName == "" || lastName.Length > 20)
            {
                txtLastName.Focus();
                success = false;
                mandatoryFields.Text = "Please enter last name (Max 20 characters)";
                return success;
            }
            if (middleInitial.Length > 1)
            {
                txtMiddleName.Focus();
                success = false;
                mandatoryFields.Text = "Middle Initial should be one character";
                return success;
            }
            if (addressLine1==""||addressLine1.Length>35)
            {
                txtAddLine1.Focus();
                success = false;
                mandatoryFields.Text = "Please enter address line 1 (Max 35 characters)";
                return success;
            }
            if (cityName == "" || cityName.Length > 25)
            {
                txtCity.Focus();
                success = false;
                mandatoryFields.Text = "Please enter city (Max 25 characters)";
                return success;
            }
            if (stateName == "" || stateName.Length != 2)
            {
                txtState.Focus();
                success = false;
                mandatoryFields.Text = "Please enter a valid state (2 characters)";
                return success;
            }
            if (zipcode == "" || zipcode.Length > 9 || zipcode.Length < 5)
            {
                txtZip.Focus();
                success = false;
                mandatoryFields.Text = "Please enter zip code (5 - 9 digits)";
                return success;
            }

            if (!(Int64.TryParse(zipcode, out zip)))
            {
                txtZip.Focus();
                success = false;
                mandatoryFields.Text = "Zipcode must have digits only";
                return success;
            }
            if (genderVal== "")
            {
                comGender.Focus();
                success = false;
                mandatoryFields.Text = "Please select gender";
                return success;
            }
            if (!(genderVal.Equals("Male")||genderVal.Equals("Female") ))
            {
                comGender.Focus();
                success = false;
                mandatoryFields.Text = "Gender shold be either Male or Female)";
                return success;
            }
            phoneNumber = phoneNumber.Replace("(", "");
            phoneNumber = phoneNumber.Replace(")", "");
            phoneNumber = phoneNumber.Replace("-", "");
            phoneNumber = phoneNumber.Replace(" ", "");
            phoneNumber = phoneNumber.Replace("/", "");

            if (phoneNumber == "" || phoneNumber.Length > 21 || phoneNumber.Length < 10)
            {
                txtPhone.Focus();
                success = false;
                mandatoryFields.Text = "Please enter phone number";
                return success;
            }
            if (!(Int64.TryParse(phoneNumber,out phone)))
            {
                txtPhone.Focus();
                success = false;
                mandatoryFields.Text = "Phone number must have digits only";
                return success;
            }
            if (emailAdd == "" || emailAdd.Length>60 )
            {
                txtEmail.Focus();
                success = false;
                mandatoryFields.Text = "Please enter email";
                return success;
            }
            if (!validateEmail(emailAdd))
            {
                txtEmail.Focus();
                success = false;
                mandatoryFields.Text = "Email format should be someone@example.com";
                return success;
            }
            if (proofOfPurchase == "")
            {
                comProof.Focus();
                success = false;
                mandatoryFields.Text = "Please select proof of Purchase";
                return success;
            }
            if (!(proofOfPurchase.Equals("Yes") || proofOfPurchase.Equals("No")))
            {
                comProof.Focus();
                success = false;
                mandatoryFields.Text = "Proof of purchase should be either Yes or No)";
                return success;
            }
            if (dateReceived == "" || dateReceived.Length!=10)
            {
                txtDate.Focus();
                success = false;
                mandatoryFields.Text = "Please enter date received (MM:DD:YYYY)";
                return success;
            }
            else
            {
                
                string validDate = dateReceived.Replace("/", "");
                if (!(Int64.TryParse(validDate, out validDay)))
                {
                    txtDate.Focus();
                    success = false;
                    mandatoryFields.Text = "Date should have digits only";
                    return success;
                }
                

                String[] dateArray = dateReceived.Split('/');
                
                if (Convert.ToInt16(dateArray[0]) < 1 || Convert.ToInt16(dateArray[0]) > 12)
                {
                    txtDate.Focus();
                    mandatoryFields.Text = "Month should be between 1-12";
                    success = false;
                    return success;
                }
                if (Convert.ToInt16(dateArray[1]) < 1 || Convert.ToInt16(dateArray[1]) > 31)
                {
                    txtDate.Focus();
                    mandatoryFields.Text = "Date should be between 1-31";
                    success = false;
                    return success;
                }
                if (Convert.ToInt16(dateArray[2]) < 1)
                {
                    txtDate.Focus();
                    mandatoryFields.Text = "Enter a valid year";
                    success = false;
                    return success;
                }
                
                
                    DateTime dateRec = Convert.ToDateTime(dateReceived);
                    string currentDate = getCurrentDate();
                    DateTime compareDay = Convert.ToDateTime(currentDate);
                    if (DateTime.Compare(dateRec, compareDay) > 0)
                    {
                        txtDate.Focus();
                        mandatoryFields.Text = "Date received cannot be a future date";
                        success = false;
                        return success;
                    }

            }
            return success;
        }

        /*This method either adds or modifies the data if the daa is valid */
        private Boolean saveData(Boolean success)
        {
            txtEndTime.Text = getCurrentDate() + " " + getCurrentTime();
            startTime = txtStartTime.Text;
            endTime = txtEndTime.Text;
            backSpaceCount = txtBackSpaceCount.Text;
            RecordDetails newRecord = new RecordDetails();
            newRecord.FirstName = firstName;
            newRecord.LastName = lastName;
            newRecord.MiddleInitial = middleInitial;
            newRecord.AddressLine1 = addressLine1;
            newRecord.AddressLine2 = addressLine2;
            newRecord.City = cityName;
            newRecord.State = stateName;
            newRecord.Zipcode = zipcode;
            newRecord.Gender = genderVal;
            newRecord.PhoneNumber = phoneNumber;
            newRecord.Email = emailAdd;
            newRecord.ProofOfPurchase = proofOfPurchase;
            newRecord.DateReceived = dateReceived;
            newRecord.StartTime = startTime;
            newRecord.EndTime = endTime;
            backSpaceCount = (backSpaceCount == "") ? "0" : backSpaceCount;
            newRecord.BackSpaceCount = Convert.ToInt32(backSpaceCount);

           
            if (success)
            {
                if (newPrimaryKey.Length > 0)
                {
                    success = service.modifyRecord(newRecord, newPrimaryKey);
                   
                }
                else
                {
                    success = service.addNewRecord(newRecord);
                   
                }
            }

           
            if (success)
            {
                service.writeData();
            }

           return success;

        }

        
        /*This method is to reset the form after adding or modifying or deleting a record or whenever the user presses the reset buttton*/
        private void resetForm()
        {
            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    ((TextBox)x).Clear();
                }
                comGender.SelectedIndex=-1;
                comProof.SelectedIndex=-1;
                this.dateReceivedToCurrentDate();

            }
         }
        
        /*This method is to update the datagrid whenever some record is added or modified or deleted*/
        private void updateGrid()
        {
            dataGridView.Rows.Clear();
            List<string[]> datagridRows = service.dataGridGenerate();
            foreach (var newRow in datagridRows)
            {
                dataGridView.Rows.Add(newRow);
            }

        }
           
        /*This method gets the current date*/
        public String getCurrentDate()
        {
            DateTime currentDay = DateTime.Today;
            String currentDate = currentDay.ToString("MM/dd/yyyy");
            getCurrentTime();
            return currentDate;
        }

        /* This method gets the current Time*/
         
        public String getCurrentTime()
        {
            DateTime currentDay = DateTime.Now;
            String currentTime = currentDay.ToString("HH:mm:ss");

            return currentTime;
        }

        /*This method sets the date received field default to today's date*/
        private void dateReceivedToCurrentDate()
        {

            String currentDate = getCurrentDate();
            txtDate.Text = currentDate;
        }

        /*This method updates the backspace count and records the start time of a new record */
        private void startFillUp(object sender, KeyPressEventArgs e)
        {
            if (txtStartTime.Text.Length == 0)
            {
                txtStartTime.Text = getCurrentDate() + " " + getCurrentTime();
            }

            resetBtn.Enabled = true;
            mandatoryFields.Text = "Fields marked with * are mandatory";

            if (e.KeyChar == (char)Keys.Back)
            {
                int spaceCount = (txtBackSpaceCount.Text == "") ? 0 : Convert.ToInt32(txtBackSpaceCount.Text);
                txtBackSpaceCount.Text = Convert.ToString(spaceCount + 1);
            }
        }

        /*The following two methods are events activated when user the user selets a particlar cell of a record on the datagrid generated */
        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            viewData();
            mandatoryFields.Text = "Fields marked with * are mandatory";

        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            viewData();
            mandatoryFields.Text = "Fields marked with * are mandatory";
        }

        /*This method updates the fields of the form whenever the user selets a record on the datagrid*/
        private void viewData()
        {
            modifyBtn.Enabled = true;
            delBtn.Enabled = true;
            addBtn.Enabled = false;
            resetBtn.Enabled = true;

            int gridIndex = dataGridView.SelectedRows[0].Index;
            RecordDetails newRecord= service.getNewRecord(gridIndex);
                      
            if (newRecord == null)
            {
                return;
            }

            resetForm();          

            txtFirstName.Text = newRecord.FirstName;
            txtLastName.Text = newRecord.LastName;
            txtMiddleName.Text = newRecord.MiddleInitial;
            txtAddLine1.Text = newRecord.AddressLine1;
            txtAddLine2.Text = newRecord.AddressLine2;
            txtCity.Text = newRecord.City;
            txtState.Text = newRecord.State;
            txtZip.Text = newRecord.Zipcode;
            comGender.Text = newRecord.Gender;                    
            txtPhone.Text = newRecord.PhoneNumber;
            txtEmail.Text = newRecord.Email;
            comProof.Text = newRecord.ProofOfPurchase;
            txtDate.Text = newRecord.DateReceived;
            txtStartTime.Text = newRecord.StartTime;
            txtEndTime.Text = newRecord.EndTime;
            txtBackSpaceCount.Text = Convert.ToString(newRecord.BackSpaceCount);
            txtPrimaryKey.Text = newRecord.FirstName + " " + newRecord.LastName + " " + newRecord.PhoneNumber;

        }
                
        /*This is invoked whenever the user presses the RESET button*/
        private void resetBtn_Click(object sender, EventArgs e)
        {
            resetForm();
            modifyBtn.Enabled = false;
            delBtn.Enabled = false;
            addBtn.Enabled = true;
            resetBtn.Enabled = false;
            updateGrid();
            mandatoryFields.Text = "Fields marked with * are mandatory";
        }

        /*This method validates the email entered by the user*/
        private Boolean validateEmail(string mail)
        {
            string pattern = null;
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(mail, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
