using RoomManagementDataAccess;
using RoomManagementModels;
using System.Collections.Generic;
using System.Configuration;

namespace RoomManagement.BusinessLayer
{
    /// <summary>
    /// Room Management Business layer class
    /// </summary>
    public class RoomBL : IRoom
    {
        #region Location
        /// <summary>
        /// Add location details
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool AddLocation(LocationModel location)
        {
            bool result = false;
            RoomDL room = new RoomDL();
            result = room.AddLocation(location);
            return result;
        }

        /// <summary>
        /// Get location details
        /// </summary>
        /// <returns></returns>
        public List<LocationModel> GetLocation()
        {
            List<LocationModel> result = null;
            RoomDL room = new RoomDL();
            result = room.GetLocation();
            return result;
        }
        #endregion

        #region Room and booking Details
        /// <summary>
        /// Add Room details
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool AddRoom(RoomModel room)
        {
            bool result = false;
            RoomDL roomDA = new RoomDL();
            result = roomDA.AddRoom(room);
            return result;
        }


        /// <summary>
        /// Add Room details
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<RoomModel> GetRoom()
        {
            List<RoomModel> result = null;
            RoomDL roomDA = new RoomDL();
            result = roomDA.GetRoom();
            return result;
        }

        /// <summary>
        /// Add Booking Details
        /// </summary>
        /// <param name="roomBookingDetails"></param>
        /// <returns></returns>
        public bool AddRoomBookingDetails(RoomBookingModel roomBookingDetails)
        {
            bool result = false;
            RoomDL room = new RoomDL();
            result = room.AddRoomBookingDetails(roomBookingDetails);
            return result;
        }
        #endregion

        #region Dashboard

        /// <summary>
        /// Get Room details for DashBoard
        /// </summary>
        /// <returns></returns>
        public dynamic GetDashBoard()
        {
            RoomDL room = new RoomDL();
            dynamic result = room.GetBookingDashboard();
            return result;
        }
        #endregion
    }
}
