using RoomManagementModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RoomManagementDataAccess
{
    public class RoomDL
    {
        #region variable
        string connectionString = null;
        #endregion

        #region Constructor
        public RoomDL()
        {           
            connectionString = @"Data Source=np:\\.\pipe\LOCALDB#C0942EDA\tsql\query;Initial Catalog=RoomManagement;Integrated Security=True";            
        }
        #endregion

        #region Location
        /// <summary>
        /// Add location detail into Database
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool AddLocation(LocationModel location)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_insertLocationDetails", sqlConnection);
            try
            {
                sqlConnection.Open();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@LocationName", location.LocationName));
                sqlCommand.Parameters.Add(new SqlParameter("@User", location.CreatedBy));                
                sqlCommand.ExecuteNonQuery();
                result = true;               
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                result = false;
            }
            finally
            {
                sqlConnection.Close();                
            }
            return result;
        }

        /// <summary>
        /// Get Location details from Database
        /// </summary>
        /// <returns></returns>
        public List<LocationModel> GetLocation()
        {
            List<LocationModel> lstLocation = new List<LocationModel>();
            LocationModel location;            
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_GetLocationDetails", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {                                              
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                   location = new LocationModel(int.Parse(dataTable.Rows[i]["LocationId"].ToString()), dataTable.Rows[i]["LocationName"].ToString());                   
                   lstLocation.Add(location);
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                lstLocation = null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return lstLocation;
        }
        #endregion
        
        #region Dashboard
        /// <summary>
        /// Get the Room and boooking details for Dashboard display 
        /// </summary>
        /// <returns></returns>
        public DataTable GetBookingDashboard()
        {            
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_GetRoomBookingDashboardDetails", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                dataTable = null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataTable;
        }
        #endregion

        #region Room and Booking

        public bool AddRoom(RoomModel room)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_insertRoomDetails", sqlConnection);
            try
            {               
                sqlConnection.Open();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@Location", room.LocationID));
                sqlCommand.Parameters.Add(new SqlParameter("@RoomName", room.RoomName));
                sqlCommand.Parameters.Add(new SqlParameter("@RoomAddress", room.RoomAddress));
                sqlCommand.Parameters.Add(new SqlParameter("@Capacity", room.Capacity));
                sqlCommand.Parameters.Add(new SqlParameter("@genderSpecfic", room.IsGenderSpecific));
                sqlCommand.Parameters.Add(new SqlParameter("@sharedRoom", room.IsSharedRoom));
                sqlCommand.Parameters.Add(new SqlParameter("@User", room.CreatedBy));
                sqlCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                result = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return result;
        }


        /// <summary>
        /// Get Room details from Database
        /// </summary>
        /// <returns></returns>
        public List<RoomModel> GetRoom()
        {
            List<RoomModel> lstRoom = new List<RoomModel>();
            RoomModel room;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_GetRoomDetails", sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            try
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    room = new RoomModel();
                    room.RoomID = int.Parse(dataTable.Rows[i]["RoomId"].ToString());
                    room.RoomName = dataTable.Rows[i]["RoomName"].ToString();
                    room.IsGenderSpecific = dataTable.Rows[i]["IsGenderSpecific"].ToString();
                    lstRoom.Add(room);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                lstRoom = null;
            }
            finally
            {
                sqlConnection.Close();
            }
            return lstRoom;
        }

        public bool AddRoomBookingDetails(RoomBookingModel room)
        {
            bool result = false;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_insertRoomBookingDetails", sqlConnection);
            try
            {
                sqlConnection.Open();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@RoomID", room.RoomID));
                sqlCommand.Parameters.Add(new SqlParameter("@GuestName", room.GuestName));
                sqlCommand.Parameters.Add(new SqlParameter("@age", room.Age));
                sqlCommand.Parameters.Add(new SqlParameter("@sex", room.Sex));
                sqlCommand.Parameters.Add(new SqlParameter("@bookingStartDate", room.BookingStartDate));
                sqlCommand.Parameters.Add(new SqlParameter("@bookingEndDate", room.BookingEndDate));
                sqlCommand.Parameters.Add(new SqlParameter("@User", room.CreatedBy));
                sqlCommand.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                result = false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return result;
        }
        #endregion

    }
}
