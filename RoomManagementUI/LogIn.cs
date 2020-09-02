using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using System.Web.Security;
using RoomManagementUI.Helper;

namespace RoomManagementUI
{
    public partial class LogInForm : Form
    {
        #region Constructor
        public LogInForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Login button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Validate the user name and password
            if (IsValidationLogInDetails())
            {
                //Call RestAPI for Authentication and get the user role
                string role = Helper.RestAPICall.GetUserRole(txtName.Text, txtPassword.Text);
                if (string.IsNullOrEmpty(role) && ( ! role.Contains("User") || !role.Contains("Admin"))) 
                {
                    MessageBox.Show("Invalid User Name and Password. Please try again", Constants.ApplicationName);
                    txtName.Focus();
                    return;
                }
                else
                {
                    //After Authentication successfully completed, Call the Dashboard form
                    Constants.UserName = txtName.Text; //Assign the user name
                    Constants.UserRole = role; //Assign user role
                    this.Hide();
                    DashBoardForm dashboard = new DashBoardForm();
                    dashboard.ShowDialog();
                }
            }
        }
        #endregion

        #region Validation

        /// <summary>
        /// Validate the input fields
        /// </summary>
        /// <returns></returns>
        private bool IsValidationLogInDetails()
        {
            bool validationResult = false;
            //user name validation
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Kindly provide the User Name.", Constants.ApplicationName);
                txtName.Focus();
                return validationResult;
            }
            //password validation
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Kindly provide the Passwird.", Constants.ApplicationName);
                txtPassword.Focus();
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
