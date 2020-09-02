using RoomManagementModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomManagementUI
{
    public partial class RoomBookingForm : Form
    {
        #region Variable
        List<RoomModel> roomModels = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Bind the control values
        /// </summary>
        /// <param name="roomId"></param>
        public RoomBookingForm(string roomId)
        {
            InitializeComponent();
            roomModels = Helper.RestAPICall.BindRoomData();
            BindRoom();
            BindForm();
            cmbRoom.SelectedValue = int.Parse(roomId);            
        }
        #endregion

        #region Events
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            //Validate form controls
            if (IsValidationRoomDetails())
            {
                //Get the selected Room data
                var room = roomModels.Where(r => r.RoomID == int.Parse(cmbRoom.SelectedValue.ToString())).ToList();
                RoomModel roomModel = room[0];
                //Validate Gender specific Room.
                if (roomModel.IsGenderSpecific == "B" || roomModel.IsGenderSpecific == cmbGender.SelectedItem.ToString().Substring(0,1))
                {

                    string message = Helper.RestAPICall.AddRoomBooking(cmbRoom.SelectedValue.ToString(), 
                                        txtName.Text, ntxtAge.Value.ToString(), cmbGender.SelectedItem.ToString(),
                                        dtpStartDate.Value, dtpEndDate.Value, Constants.UserName); //API call to Save Booking details in DB
                    MessageBox.Show(message, Constants.ApplicationName);
                    this.Close();
                }
                else
                {
                    string member = null;
                    if (roomModel.IsGenderSpecific == "F")
                        member = "Female";
                    else
                        member = "Male";

                    MessageBox.Show("Gender specific Room! Only " + member + " is able to book.", Constants.ApplicationName);
                    cmbGender.Focus();
                }
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Bind the Form data
        /// </summary>
        private void BindForm()
        {            
            BindDateControls();
            BindGender();
        }

        /// <summary>
        /// Bind Room details in Drop down box
        /// </summary>
        private void BindRoom()
        {
            cmbRoom.ValueMember = "RoomId";
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.DataSource = roomModels; 
            cmbRoom.Enabled = false; //Fix the Room value
        }

        /// <summary>
        /// Assign format value to Date and Time Control
        /// </summary>
        private void BindDateControls()
        {
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.CustomFormat = "dd MMM yyyy"; //Date format

            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpEndDate.CustomFormat = "dd MMM yyyy"; //Date format

            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.CustomFormat = "HH:mm:ss tt"; //Time format

            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = "HH:mm:ss tt"; //Time format
        }

        /// <summary>
        /// Bind Gender information
        /// </summary>
        private void BindGender()
        {
            List<string> gender = new List<string>();
            gender.Add("Male");
            gender.Add("Female");
            gender.Add("Both");
            
            cmbGender.DataSource = gender; //assign gender list into drop down 
            cmbGender.SelectedText = "Both";

        }

        /// <summary>
        /// Validate the each input fields
        /// </summary>
        /// <returns></returns>
        private bool IsValidationRoomDetails()
        {
            bool validationResult = false;
            //Validate Room Dropdwon
            if (cmbRoom.SelectedIndex < 0)
            {
                MessageBox.Show("Kindly select the Room details.", Constants.ApplicationName);
                cmbRoom.Focus();
                return validationResult;
            }
            //Validate Room Name
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Kindly provide the Guest Name.", Constants.ApplicationName);
                txtName.Focus();
                return validationResult;
            }
            //Validate Age value
            else if (string.IsNullOrEmpty(ntxtAge.Value.ToString()))
            {
                MessageBox.Show("Kindly select the valid Age.", Constants.ApplicationName);
                ntxtAge.Focus();
                return validationResult;
            }
            //validate gender name
            else if (cmbGender.SelectedIndex < 0)
            {
                MessageBox.Show("Kindly select the Gender.", Constants.ApplicationName);
                cmbGender.Focus();
                return validationResult;
            }            
            else
            {
                validationResult = true;
            }
            return validationResult;
        }
        #endregion

    }
}
