using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RoomManagementModels;

namespace RoomManagementUI
{
    public partial class DashBoardForm : Form
    {

        #region Variables
        // Declared for the Location grid
        DataGridView LocationDataView = new DataGridView();
        // Declared for the Room grid
        DataGridView RoomDataView = new DataGridView();             
        Helper.DataGridHelper dataGridHelper = new Helper.DataGridHelper();      
        public int ColumnIndex;
        DataTable dtName = new DataTable();
        #endregion

        #region Constructor
        public DashBoardForm()
        {
            InitializeComponent(); //Initialize the componet
            
        }
        #endregion

        #region Events        
        /// <summary>
        /// Load and Bind all controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DashBoardForm_Load(object sender, EventArgs e)
        {
            if (Constants.UserRole == "Admin")
                btnAddRoom.Visible = true;
            ScrollBarSetting();
            BindForm();
            LocationGrid_Initialize(Helper.DataGridHelper.objRoomList);
            RoomGrid_Initialize();
            BindDateControls();
        }

        /// <summary>
        /// Search Room details based on selected location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = Helper.DateTimeHelper.GetDateTimeValue(dtpStartDate, dtpStartTime);
            DateTime endDate = Helper.DateTimeHelper.GetDateTimeValue(dtpEndDate, dtpEndTime);
            dataGridHelper.BindDashBoardData(startDate, endDate);
            int locationId = int.Parse(cmbLocation.SelectedValue.ToString());
            //Room model is matched with location id
            List<RoomModel> roomModels = Helper.DataGridHelper.objRoomList.Where(l => l.LocationID == locationId).ToList();
            LocationDataView.DataSource = roomModels;
        }

        /// <summary>
        /// Show the Admin form for add Room and Location details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.ShowDialog();
            BindForm();           
        }

        /// <summary>
        /// Refresh the Dashboard grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime startDate = Helper.DateTimeHelper.GetDateTimeValue(dtpStartDate, dtpStartTime);
            DateTime endDate = Helper.DateTimeHelper.GetDateTimeValue(dtpEndDate, dtpEndTime);
            dataGridHelper.BindDashBoardData(startDate, endDate);
            BindLocation();
            LocationDataView.DataSource = Helper.DataGridHelper.objRoomList;
        }

        /// <summary>
        /// Close application once your click on the Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DashBoardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Methods

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
        /// Bind the controls
        /// /// </summary>
        private void BindForm()
        {
            dataGridHelper.BindImage();
            DateTime startDate = Helper.DateTimeHelper.GetDateTimeValue(dtpStartDate, dtpStartTime);
            DateTime endDate = Helper.DateTimeHelper.GetDateTimeValue(dtpEndDate, dtpEndTime);
            dataGridHelper.BindDashBoardData(startDate, endDate);
            BindLocation();
            LocationDataView.DataSource = Helper.DataGridHelper.objRoomList;            
        }

        /// <summary>
        /// Set the scroll - Data grid view
        /// </summary>
        private void ScrollBarSetting()
        {
            LocationDataView.ScrollBars = ScrollBars.Vertical;
            LocationDataView.ScrollBars = ScrollBars.Both;
        }

        /// <summary>
        /// Call RestAPI and bind result in Location dropdown box
        /// </summary>
        private void BindLocation()
        {
            List<LocationModel> locations = Helper.RestAPICall.BindLocation();
            cmbLocation.ValueMember = "LocationId";
            cmbLocation.DisplayMember = "LocationName";
            cmbLocation.DataSource = locations;
        }

        /// <summary>
        /// Initialize the Room/Main grid
        /// </summary>
        /// <param name="objRoomList"></param>
        private void LocationGrid_Initialize(List<RoomModel> objRoomList)
        {
            //First generate the grid Layout Design
            Helper.DataGridHelper.Layouts(LocationDataView, Color.LightSteelBlue, Color.AliceBlue, Color.WhiteSmoke, false, Color.SteelBlue, false, false, false);

            //Set Height,width and add panel to your selected control
            Helper.DataGridHelper.GenerateDataGrid(LocationDataView, pnlDashBoard, 1000, 600, 10, 10);

            // Color Image Column creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.ImageColumn, "img", "", "", true, 40, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation with hiddern column
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "LocationId", "Location Id", "Location Id",
                                    false, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                                    Color.Transparent, null, "", "", Color.Black);
            // BoundColumn creation with hiddern column
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "RoomID", "Room Id", "Room Id",
                                    false, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                                    Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "LocationName", "Location Name", "Location Name",
                                    true, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, 
                                    Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "RoomName", "Room Name", 
                "Room Name", true, 150, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, 
                Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "RoomAddress", "Room Address",
                "Room Address", true, 200, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                Color.Transparent, null, "", "", Color.Black);

            // Image column creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.AvailableImageColumn, "AvailableStatus", "AvailableStatus",
                "AvailableStatus", true, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                Color.Transparent, null, "", "", Color.Black);
            //BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "AvailableStatus", "AvailableStatus",
                "AvailableStatus", false, 100, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(LocationDataView, Helper.DataGridHelper.FormControls.BoundColumn, "Capacity", "Capacity",
                "Capacity", true, 60, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter,
                Color.Transparent, null, "", "", Color.Black);

            

            //Convert the List to DataTable
            DataTable detailTableList = ListtoDataTable(Helper.DataGridHelper.objRoomBookingList);

            // Image Colum Click Event - In  this method we create an event for cell click and we will display the Detail grid with result.

            Helper.DataGridHelper helper = new Helper.DataGridHelper();
            helper.DGVMasterGridClickEvents(LocationDataView, RoomDataView, LocationDataView.Columns["img"].Index, 
                Helper.DataGridHelper.EventTypes.cellContentClick, Helper.DataGridHelper.FormControls.ImageColumn, detailTableList, "RoomId");

            // Bind data to DGV.
            LocationDataView.DataSource = objRoomList;

        }

        private void RoomGrid_Initialize()
        {
            Helper.DataGridHelper.Layouts(RoomDataView, Color.Azure, Color.Wheat, Color.Tan, false, Color.Sienna, false, false, false);

            //Set Height,width and add panel to your selected control
            Helper.DataGridHelper.GenerateDataGrid(RoomDataView, pnlDashBoard, 800, 200, 10, 10);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(RoomDataView, Helper.DataGridHelper.FormControls.BoundColumn, "GuestName", "GuestName", "GuestName", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(RoomDataView, Helper.DataGridHelper.FormControls.BoundColumn, "Age", "Age", "Age", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);
            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(RoomDataView, Helper.DataGridHelper.FormControls.BoundColumn, "Sex", "Sex", "Sex", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);
            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(RoomDataView, Helper.DataGridHelper.FormControls.BoundColumn, "BookingStartDate", "BookingStartDate", "BookingStartDate", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);
            // BoundColumn creation
            Helper.DataGridHelper.Templatecolumn(RoomDataView, Helper.DataGridHelper.FormControls.BoundColumn, "BookingEndDate", "BookingEndDate", "BookingEndDate", true, 90, DataGridViewTriState.True, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight, Color.Transparent, null, "", "", Color.Black);

            Helper.DataGridHelper helper = new Helper.DataGridHelper();
            helper.DGVDetailGridClickEvents(RoomDataView);

        }

        /// <summary>
        /// Covert the List into DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DetailList"></param>
        /// <returns></returns>
        private static DataTable ListtoDataTable<T>(IEnumerable<T> DetailList)
        {
            Type type = typeof(T);
            var typeproperties = type.GetProperties();

            DataTable listToDT = new DataTable();
            foreach (PropertyInfo propInfo in typeproperties)
            {
                listToDT.Columns.Add(new DataColumn(propInfo.Name, propInfo.PropertyType));
            }

            foreach (T ListItem in DetailList)
            {
                object[] values = new object[typeproperties.Length];
                for (int i = 0; i < typeproperties.Length; i++)
                {
                    values[i] = typeproperties[i].GetValue(ListItem, null);
                }

                listToDT.Rows.Add(values);
            }

            return listToDT;
        }


        #endregion

       
    }
}
