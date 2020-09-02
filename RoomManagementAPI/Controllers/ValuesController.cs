using Newtonsoft.Json.Linq;
using RoomManagement.BusinessLayer;
using RoomManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoomManagementAPI.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Add Location Restful API
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddLocation")]
        public IHttpActionResult AddLocation([FromBody]dynamic requestBody)
        {
            try
            {
                string columnsData = Convert.ToString(requestBody);
                if (!string.IsNullOrEmpty(columnsData))
                {

                    var columns = JObject.Parse(columnsData);
                    LocationModel location = new LocationModel();
                    location.LocationName = columns["locationName"].ToString();
                    location.CreatedBy = columns["userId"].ToString();

                    RoomBL room = new RoomBL();
                    room.AddLocation(location);
                }


                return Ok("Location is added successfully.");
            }
            catch (Exception ex)
            {
                string msg = "Room Details are failed to add. \n Error Message: " + ex.Message;
                return Ok(msg);
            }
        }


        /// <summary>
        /// Get Location Restful API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetLocation")]
        public IHttpActionResult GetLocation()
        {
            IList<LocationModel> locationList = null;
            try
            {
                RoomBL room = new RoomBL();
                locationList = room.GetLocation();

                if (locationList.Count == 0)
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(locationList);
        }

        /// <summary>
        /// Get Room Restful API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetRoom")]
        public IHttpActionResult GetRoom()
        {
            IList<RoomModel> roomList = null;
            try
            {
                RoomBL room = new RoomBL();
                roomList = room.GetRoom();

                if (roomList.Count == 0)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(roomList);
        }

        /// <summary>
        /// Add Room Restful API
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddRoom")]
        public IHttpActionResult AddRoom([FromBody]dynamic requestBody)
        {
            try
            {
                string columnsData = Convert.ToString(requestBody);
                if (!string.IsNullOrEmpty(columnsData))
                {

                    var columns = JObject.Parse(columnsData);
                    RoomModel roomModel = new RoomModel();
                    roomModel.LocationID = int.Parse(columns["locationId"].ToString());
                    roomModel.RoomName = columns["roomName"].ToString();
                    roomModel.RoomAddress = columns["roomAddress"].ToString();
                    roomModel.Capacity = int.Parse(columns["capacity"].ToString());
                    roomModel.IsGenderSpecific = columns["genderSpecific"].ToString();                    
                    roomModel.CreatedBy = columns["user"].ToString();

                    RoomBL room = new RoomBL();
                    room.AddRoom(roomModel);
                }


                return Ok("Room Details are added successfully.");
            }
            catch (Exception ex)
            {
                string msg = "Room Details are failed to add. \n Error Message: " + ex.Message;
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Add Room booking API
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddRoomBooking")]
        public IHttpActionResult AddRoomBooking([FromBody]dynamic requestBody)
        {
            try
            {
                string columnsData = Convert.ToString(requestBody);
                if (!string.IsNullOrEmpty(columnsData))
                {

                    var columns = JObject.Parse(columnsData);
                    RoomBookingModel roomBookingModel = new RoomBookingModel();
                    roomBookingModel.RoomID = int.Parse(columns["roomID"].ToString());
                    roomBookingModel.GuestName = columns["guestName"].ToString();                    
                    roomBookingModel.Age = int.Parse(columns["age"].ToString());
                    roomBookingModel.Sex = columns["sex"].ToString();
                    roomBookingModel.BookingStartDate = DateTime.Parse(columns["bookStartDate"].ToString());
                    roomBookingModel.BookingEndDate = DateTime.Parse(columns["bookEndDate"].ToString());
                    roomBookingModel.CreatedBy = columns["user"].ToString();

                    RoomBL room = new RoomBL();
                    room.AddRoomBookingDetails(roomBookingModel);
                }


                return Ok("Room Booking Details are added successfully.");
            }
            catch (Exception ex)
            {
                string msg = "Room Booking Details are failed to add. \n Error Message: " + ex.Message;
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get infromation for Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetDashboard")]
        public IHttpActionResult GetDashboard()
        {            
            try
            {
                RoomBL room = new RoomBL();
                var dashboard = room.GetDashBoard();
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get infromation for Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetUserRole")]
        public IHttpActionResult GetUserRole(string userName, string userPassword)
        {
            try
            {
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                
                UserBL user = new UserBL();
                var userRole = user.GetUserRole(userName, userPassword);
                return Ok(userRole);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }
    }
}
