using RoomManagementModels;
using System; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RoomManagementUI
{
    public partial class AdminForm : Form
    {

        #region Variables
        List<LocationModel> locations = null;
        List<RoomModel> roomModels = null;
        #endregion

        #region Constructor

        public AdminForm()
        {
            roomModels = Helper.RestAPICall.BindRoomData();
            InitializeComponent();
            BindForm();
            BindGender();
        }
        #endregion

        #region Events
        /// <summary>
        /// Add location name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            //Location name - empty validation
            if (string.IsNullOrEmpty(txtLocationName.Text))
            {
                MessageBox.Show("Kindly enter the location name.", Constants.ApplicationName);
                txtLocationName.Focus();
                return;
            }
            //Location name is already exist in DB validation
            var location = locations.Where(l => l.LocationName.ToLower() == txtLocationName.Text.ToLower() && !string.IsNullOrEmpty(txtLocationName.Text)).ToList();
            if (location.Count == 0)
            {
                AddLocation();
            }
            else
            {
                MessageBox.Show("Location Name is already available.", Constants.ApplicationName);
                txtLocationName.Focus();
                return;
            }

        }

        //Enable the Add location section
        private void lblAddLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gbLocation.Visible = true;
        }

        /// <summary>
        /// Add Room values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            //Validate the input data
            if (IsValidationRoomDetails())
            {
                //Validate room value is existing in DB
                var room = roomModels.Where(r => r.RoomName.ToLower() == txtName.Text.ToLower() 
                                            && !string.IsNullOrEmpty(txtName.Text)
                                            && !string.IsNullOrEmpty(cmbLocation.SelectedText)).ToList();
                //Not available and Call Rest API for Insertion
                if (room.Count == 0)
                {
                    string message = Helper.RestAPICall.AddRoom(cmbLocation.SelectedValue.ToString(), txtName.Text, txtAddress.Text,
                    ntxtCapacity.Value.ToString(), cmbGender.SelectedItem.ToString(), "User");
                    MessageBox.Show(message, Constants.ApplicationName);
                    this.Close();
                }
                else
                {
                    //Validation message 
                    MessageBox.Show("Room Name is already available.", Constants.ApplicationName);
                    txtName.Focus(); //set foucs to control
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Bind the form control with default values
        /// </summary>
        private void BindForm()
        {
            gbLocation.Visible = false;
            BindLocation();       
        }

        /// <summary>
        /// Bind the gender values
        /// </summary>
        private void BindGender()
        {
            List<string> gender = new List<string>();
            gender.Add("Male");
            gender.Add("Female");
            gender.Add("Both");

            cmbGender.DataSource = gender; //assign the data source
            cmbGender.SelectedIndex = 2; //select default value

        }

        /// <summary>
        /// Input field validations
        /// </summary>
        /// <returns></returns>
        private bool IsValidationRoomDetails()
        {
            bool validationResult = false;
            //Location validation
            if (cmbLocation.SelectedIndex < 0)
            {
                MessageBox.Show("Kindly select the Location details.", Constants.ApplicationName);
                cmbLocation.Focus();
                return validationResult;
            }
            //Null or empty validation for Room name
            else if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Kindly provide the Room Name.", Constants.ApplicationName);
                txtName.Focus();
                return validationResult;
            }
            //Null or empty validation for Address
            else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Kindly provide the Room Address.", Constants.ApplicationName);
                txtAddress.Focus();
                return validationResult;
            }
            //Null or empty validation for Capacity
            else if (string.IsNullOrEmpty(ntxtCapacity.Text))
            {
                MessageBox.Show("Kindly select the Room Capacity.", Constants.ApplicationName);
                ntxtCapacity.Focus();
                return validationResult;
            }
            //Capacity count validation
            else if (int.Parse(ntxtCapacity.Text) <= 0 )
            {
                MessageBox.Show("Kindly select the proper Room Capacity.", Constants.ApplicationName);
                ntxtCapacity.Focus();
                return validationResult;
            }
            //gender field selection validation
            else if (cmbGender.SelectedIndex < 0)
            {
                MessageBox.Show("Kindly select the Gender details.", Constants.ApplicationName);
                cmbGender.Focus();
                return validationResult;
            }
            else
            {
                validationResult = true;
            }
            return validationResult;
        }

       
        /// <summary>
        /// Call Rest API to get the Location data and bind the location value in drop down box
        /// </summary>
        private void BindLocation()
        {            
            locations = Helper.RestAPICall.BindLocation();
            cmbLocation.ValueMember = "LocationId";
            cmbLocation.DisplayMember = "LocationName";
            cmbLocation.DataSource = locations;
            cmbLocation.SelectedIndex = 0;
        }


        /// <summary>
        /// Call Rest API for save location data in DB
        /// </summary>
        private void AddLocation()
        {
            //Rest API call for Add location info into DB
            string result = Helper.RestAPICall.AddLocation(txtLocationName.Text, Constants.UserName); 
            if (!string.IsNullOrEmpty(result))
            {
                MessageBox.Show(result.ToString(), Constants.ApplicationName);
                BindLocation();
                txtLocationName.Text = "";
                gbLocation.Visible = false;
            }
            else
            {
                MessageBox.Show("Unable to add user location.");
            }
            
        }

        #endregion
    }

}   
