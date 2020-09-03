using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using RoomManagementModels;


namespace RoomManagementUI.Helper
{   
    public class DataGridHelper
    {
        #region Enum

        public enum FormControls
        {
            BoundColumn,
            TextBox,
            ComboBox,
            CheckBox,
            DateTimepicker,
            Button,
            NumericTextBox,
            ColorDialog,
            ImageColumn,
            AvailableImageColumn
        }

        public enum EventTypes
        {
            CellClick,
            cellContentClick,
            EditingControlShowing
        }
        #endregion

        #region Variables
        public DataGridView LocationDataGrid = new DataGridView();
        
        public DataGridView RoomDataGrid = new DataGridView();
        public static ImageList statusList = new ImageList();
        

        static String ImageName = "toggle.png";
        String FilterColumnName = "";
        DataTable DetailgridDT;
        int gridColumnIndex = 0;

        public static List<RoomBookingModel> objRoomBookingList = new List<RoomBookingModel>();
        public static List<RoomModel> objRoomList = new List<RoomModel>();
        #endregion

        /// <summary>
        /// Bind images to display in Grid
        /// </summary>
        public void BindImage()
        {
            System.Drawing.Image availableImage = Image.FromFile("Available.png");
            System.Drawing.Image NotAvaialbleImage = Image.FromFile("NotAvailable.jpg");
            System.Drawing.Image bookImage = Image.FromFile("cart.jpg");
            System.Drawing.Image detailImage = Image.FromFile("expand.png");

            statusList.Images.Add(availableImage);
            statusList.Images.Add(NotAvaialbleImage);
            statusList.Images.Add(bookImage);
            statusList.Images.Add(detailImage);
        }

        /// <summary>
        /// Call REST API and get hte Room and Booking details
        /// </summary>
        public void BindDashBoardData(DateTime startDate, DateTime endDate)
        {
            
            var data = Helper.RestAPICall.GetDashboardData(startDate, endDate);
            //Handle null values for Json conversion
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            objRoomList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoomModel>>(data, settings);
            objRoomBookingList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoomBookingModel>>(data, settings);


        }

        public void GetDataByLocation()
        {

        }
        #region Layout
        /// <summary>
        /// Data grid design layout
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="BackgroundColor"></param>
        /// <param name="RowsBackColor"></param>
        /// <param name="AlternatebackColor"></param>
        /// <param name="AutoGenerateColumns"></param>
        /// <param name="HeaderColor"></param>
        /// <param name="HeaderVisual"></param>
        /// <param name="RowHeadersVisible"></param>
        /// <param name="AllowUserToAddRows"></param>
        public static void Layouts(DataGridView dataGridView, Color BackgroundColor, Color RowsBackColor, Color AlternatebackColor,
                                    Boolean AutoGenerateColumns, Color HeaderColor, Boolean HeaderVisual, 
                                    Boolean RowHeadersVisible, Boolean AllowUserToAddRows)
        {
            //Grid Back ground Color
            dataGridView.BackgroundColor = BackgroundColor;

            //Grid Row BackColor
            dataGridView.RowsDefaultCellStyle.BackColor = RowsBackColor;

            //GridColumnStylesCollection Alternate Rows Backcolr
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = AlternatebackColor;

            // Auto generated here set to tru or false.
            dataGridView.AutoGenerateColumns = AutoGenerateColumns;
           
            //Column Header back Color
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;

            //Column Header Fore color  
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //header Visisble
            dataGridView.EnableHeadersVisualStyles = HeaderVisual;

            // Enable the row header
            dataGridView.RowHeadersVisible = RowHeadersVisible;

            // to Hide the Last Empty row here we use false.
            dataGridView.AllowUserToAddRows = AllowUserToAddRows;
        }
        #endregion

        /// <summary>
        /// Add grid view control
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="cntrlName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="xval"></param>
        /// <param name="yval"></param>
        public static void GenerateDataGrid(DataGridView dataGridView, Control cntrlName, int width, int height, int xval, int yval)
        {   
            dataGridView.Location = new Point(xval, yval);
            dataGridView.Size = new Size(width, height);
            
            cntrlName.Controls.Add(dataGridView);           

        }

        #region Templatecolumn
        /// <summary>
        /// Bind the different column details in Data grid
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="controlTypes"></param>
        /// <param name="cntrlnames"></param>
        /// <param name="Headertext"></param>
        /// <param name="ToolTipText"></param>
        /// <param name="Visible"></param>
        /// <param name="width"></param>
        /// <param name="Resizable"></param>
        /// <param name="cellAlignment"></param>
        /// <param name="headerAlignment"></param>
        /// <param name="CellTemplateBackColor"></param>
        /// <param name="dtsource"></param>
        /// <param name="DisplayMember"></param>
        /// <param name="ValueMember"></param>
        /// <param name="CellTemplateforeColor"></param>
        public static void Templatecolumn(DataGridView dataGridView, FormControls controlTypes, String cntrlnames, String Headertext, String ToolTipText, Boolean Visible, int width, DataGridViewTriState Resizable, DataGridViewContentAlignment cellAlignment, DataGridViewContentAlignment headerAlignment, Color CellTemplateBackColor, DataTable dtsource, String DisplayMember, String ValueMember, Color CellTemplateforeColor)
        {
            switch (controlTypes)
            {
                case FormControls.CheckBox:
                    DataGridViewCheckBoxColumn dgvChk = new DataGridViewCheckBoxColumn();
                    dgvChk.ValueType = typeof(bool);
                    dgvChk.Name = cntrlnames;

                    dgvChk.HeaderText = Headertext;
                    dgvChk.ToolTipText = ToolTipText;
                    dgvChk.Visible = Visible;
                    dgvChk.Width = width;
                    dgvChk.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvChk.Resizable = Resizable;
                    dgvChk.DefaultCellStyle.Alignment = cellAlignment;
                    dgvChk.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvChk.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvChk.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    dataGridView.Columns.Add(dgvChk);
                    break;
                case FormControls.BoundColumn:
                    DataGridViewColumn dgvbound = new DataGridViewTextBoxColumn();
                    dgvbound.DataPropertyName = cntrlnames;
                    dgvbound.Name = cntrlnames;
                    dgvbound.HeaderText = Headertext;
                    dgvbound.ToolTipText = ToolTipText;
                    dgvbound.Visible = Visible;
                    dgvbound.Width = width;
                    dgvbound.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvbound.Resizable = Resizable;
                    dgvbound.DefaultCellStyle.Alignment = cellAlignment;
                    dgvbound.HeaderCell.Style.Alignment = headerAlignment;
                    dgvbound.ReadOnly = true;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvbound.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvbound.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    dataGridView.Columns.Add(dgvbound);
                    break;
                case FormControls.TextBox:
                    DataGridViewTextBoxColumn dgvText = new DataGridViewTextBoxColumn();
                    dgvText.ValueType = typeof(decimal);
                    dgvText.DataPropertyName = cntrlnames;
                    dgvText.Name = cntrlnames;
                    dgvText.HeaderText = Headertext;
                    dgvText.ToolTipText = ToolTipText;
                    dgvText.Visible = Visible;
                    dgvText.Width = width;
                    dgvText.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvText.Resizable = Resizable;
                    dgvText.DefaultCellStyle.Alignment = cellAlignment;
                    dgvText.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvText.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvText.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    dataGridView.Columns.Add(dgvText);
                    break;
                case FormControls.ComboBox:
                    DataGridViewComboBoxColumn dgvcombo = new DataGridViewComboBoxColumn();
                    dgvcombo.ValueType = typeof(decimal);
                    dgvcombo.Name = cntrlnames;
                    dgvcombo.DataSource = dtsource;
                    dgvcombo.DisplayMember = DisplayMember;
                    dgvcombo.ValueMember = ValueMember;
                    dgvcombo.Visible = Visible;
                    dgvcombo.Width = width;
                    dgvcombo.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvcombo.Resizable = Resizable;
                    dgvcombo.DefaultCellStyle.Alignment = cellAlignment;
                    dgvcombo.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvcombo.CellTemplate.Style.BackColor = CellTemplateBackColor;

                    }
                    dgvcombo.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    dataGridView.Columns.Add(dgvcombo);
                    break;

                case FormControls.Button:
                    DataGridViewButtonColumn dgvButtons = new DataGridViewButtonColumn();
                    dgvButtons.Name = cntrlnames;
                    dgvButtons.FlatStyle = FlatStyle.Popup;
                    dgvButtons.DataPropertyName = cntrlnames;
                    dgvButtons.Visible = Visible;
                    dgvButtons.Width = width;
                    dgvButtons.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvButtons.Resizable = Resizable;
                    dgvButtons.DefaultCellStyle.Alignment = cellAlignment;
                    dgvButtons.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvButtons.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvButtons.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    dataGridView.Columns.Add(dgvButtons);
                    break;
                case FormControls.ImageColumn:
                    DataGridViewImageColumn dgvnestedBtn = new DataGridViewImageColumn();
                    dgvnestedBtn.Name = cntrlnames;
                    ImageName = "expand.png";

                    dgvnestedBtn.Image = Image.FromFile(ImageName);                    
                    dgvnestedBtn.Visible = Visible;
                    dgvnestedBtn.Width = width;
                    dgvnestedBtn.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvnestedBtn.Resizable = Resizable;
                    dgvnestedBtn.DefaultCellStyle.Alignment = cellAlignment;
                    dgvnestedBtn.HeaderCell.Style.Alignment = headerAlignment;
                    dataGridView.Columns.Add(dgvnestedBtn);
                    break;
                case FormControls.AvailableImageColumn:
                    DataGridViewImageColumn btnStatus = new DataGridViewImageColumn();
                    btnStatus.Name = cntrlnames;
                    ImageName = "Available.png";

                    btnStatus.Image = Image.FromFile(ImageName);
                    btnStatus.Visible = Visible;
                    btnStatus.Width = width;
                    btnStatus.SortMode = DataGridViewColumnSortMode.Automatic;
                    btnStatus.Resizable = Resizable;
                    btnStatus.DefaultCellStyle.Alignment = cellAlignment;
                    btnStatus.HeaderCell.Style.Alignment = headerAlignment;
                    dataGridView.Columns.Add(btnStatus);
                    break;

            }




            }

        #endregion

        #region grid Click Event
        /// <summary>
        /// Assign the Main and Detail grid valules and create cell click events
        /// </summary>
        /// <param name="masterDataGrid"></param>
        /// <param name="detailDataGrid"></param>
        /// <param name="columnIndexs"></param>
        /// <param name="eventtype"></param>
        /// <param name="types"></param>
        /// <param name="DetailTable"></param>
        /// <param name="FilterColumn"></param>
        public void DGVMasterGridClickEvents(DataGridView masterDataGrid, DataGridView detailDataGrid,int columnIndexs, 
                                            EventTypes eventtype, FormControls types, DataTable DetailTable, String FilterColumn)
        {
            LocationDataGrid = masterDataGrid;
            RoomDataGrid = detailDataGrid;
            gridColumnIndex = columnIndexs;
            DetailgridDT = DetailTable;
            FilterColumnName = FilterColumn;

            LocationDataGrid.CellContentClick += new DataGridViewCellEventHandler(LocationDataGrid_CellContentClick_Event);
            LocationDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(LocationDataGrid_CellFormatting);


        }

        /// <summary>
        /// Handle the data/image binding in data grid cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LocationDataGrid_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (LocationDataGrid.Columns[e.ColumnIndex].Name == "AvailableStatus")
                {
                    if ((this.LocationDataGrid.Rows[e.RowIndex].Cells[7].Value).ToString() == "Available")
                    {
                        e.Value = statusList.Images[0];
                    }
                    else
                    {
                        e.Value = statusList.Images[1];
                    }
                }

                if (LocationDataGrid.Columns[e.ColumnIndex].Name == "img")
                {
                    if ((this.LocationDataGrid.Rows[e.RowIndex].Cells[7].Value).ToString() == "Available")
                    {
                        e.Value = statusList.Images[2]; ;
                    }
                    else
                    {
                        e.Value = statusList.Images[3];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constants.ApplicationName);
            }
        }

        /// <summary>
        /// grid cell click event -- View Booking details and Add booking form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationDataGrid_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {
            int index = 0;

            DataGridViewImageColumn cols = (DataGridViewImageColumn)LocationDataGrid.Columns[index];

            // cols.Image = Image.FromFile(ImageName);
            LocationDataGrid.Rows[e.RowIndex].Cells[index].Value = Image.FromFile("expand.png");

            if (e.ColumnIndex == gridColumnIndex)
            {

                if (ImageName == "expand.png")
                {

                    ImageName = "toggle.png";
                    // cols.Image = Image.FromFile(ImageName);
                    LocationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);
                    


                    String Filterexpression = LocationDataGrid.Rows[e.RowIndex].Cells[FilterColumnName].Value.ToString();

                    LocationDataGrid.Controls.Add(RoomDataGrid);
                    
                   
                    Rectangle dgvRectangle = LocationDataGrid.GetCellDisplayRectangle(1, e.RowIndex, false);
                    RoomDataGrid.Size = new Size(LocationDataGrid.Width - 200, 200);
                    //GetCellDisplayRctangle is not working, add our own Location logic
                    RoomDataGrid.Location = new Point(dgvRectangle.X + 45, ((LocationDataGrid.Rows[e.RowIndex].Height) * e.RowIndex)+ 10);
                    
                   
                    


                    DataView detailView = new DataView(DetailgridDT);
                    detailView.RowFilter = FilterColumnName + " = '" + Filterexpression + "'";
                    if (detailView.Count <= 0 || string.IsNullOrEmpty(detailView[0][2].ToString()))
                    {                                               
                        ImageName = "expand.png";
                        //  cols.Image = Image.FromFile(ImageName);
                        LocationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);
                        RoomDataGrid.Visible = false;
                        //MessageBox.Show("No Details Found");                        
                        RoomBookingForm roomBooking = new RoomBookingForm(Filterexpression);
                        roomBooking.ShowDialog();
                    }
                    else
                    {
                        RoomDataGrid.Visible = true;
                        RoomDataGrid.DataSource = detailView;
                    }
                }
                else
                {
                    ImageName = "expand.png";
                    //  cols.Image = Image.FromFile(ImageName);
                    LocationDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);
                    RoomDataGrid.Visible = false;
                }
            }
            else
            {
                RoomDataGrid.Visible = false;

            }
        }
        #endregion

        /// <summary>
        /// all click event for data click
        /// </summary>
        /// <param name="dataGridView"></param>
        public void DGVDetailGridClickEvents(DataGridView dataGridView)
        {
            RoomDataGrid = dataGridView;
            RoomDataGrid.CellContentClick += new DataGridViewCellEventHandler(RoomDataGrid_CellContentClick_Event);
        }

        /// <summary>
        /// Detail view cell click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomDataGrid_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Detail grid Clicked : You clicked on " + RoomDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, Constants.ApplicationName);
        }

    }
}
