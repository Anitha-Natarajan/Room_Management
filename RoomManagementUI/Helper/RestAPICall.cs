using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using RoomManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementUI.Helper
{
    public static class RestAPICall
    {
        /// <summary>
        /// Get Location from Database
        /// </summary>
        /// <returns></returns>
        public static List<LocationModel> BindLocation()
        {
            List<LocationModel> locations = null;
            dynamic data = GetInfromationFromAPI("GetLocation");            
            locations = JsonConvert.DeserializeObject<List<LocationModel>>(data);
            return locations;
        }

        public static List<RoomModel> BindRoomData()
        {
            List<RoomModel> roomModels = null;
            dynamic data = GetInfromationFromAPI("GetRoom");
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:51318/api/");
            //    var responseTask = client.GetAsync("GetRoom");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var data = result.Content.ReadAsStringAsync().Result;
            //        roomModels = JsonConvert.DeserializeObject<List<RoomModel>>(data);
            //    }
            //}
            roomModels = JsonConvert.DeserializeObject<List<RoomModel>>(data);
            return roomModels;
        }

        /// <summary>
        /// Get Dashboard data based on the booking Date and Time Filer
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static dynamic GetDashboardData(DateTime startDate, DateTime endDate)
        {
            dynamic response = null;
            try
            {
                string apiName = String.Format("GetDashboard?startDate=" + startDate + "&endDate=" + endDate); ;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAddress);
                    var responseTask = client.GetAsync(apiName);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("Unable to Process this request. Please check your data and try again. Status Code: {0}", result.StatusCode));
                    }
                    else if (result.IsSuccessStatusCode)
                    {
                        response = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        private static dynamic GetInfromationFromAPI(string apiName)
        {
            dynamic response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAddress);
                    var responseTask = client.GetAsync(apiName);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        response = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }

        public static dynamic GetUserRole(string userName, string password)
        {
            dynamic response = null;
            try
            {
                string apiName = String.Format("GetUserRole?userName=" + userName + "&userPassword=" + password); ;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.BaseAddress);
                    var responseTask = client.GetAsync(apiName);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("Unable to Process this request. Please check your data and try again. Status Code: {0}", result.StatusCode));
                    }
                    else if (result.IsSuccessStatusCode)
                    {
                        response = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


        private static string SetInfromationFromToAPI(string apiName, dynamic request)
        {
            string response = null;            
            string url = Constants.BaseAddress + apiName; 
            
            string contentType = "application/json";
            var Request = JsonConvert.SerializeObject(request);

            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                HttpContent httpContent = new StringContent(Request, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = Client.PostAsync(url, httpContent).Result;
                var tokenResult = httpResponse.Content.ReadAsStringAsync().Result;
                var statuscode = httpResponse.StatusCode;
                if (httpResponse.IsSuccessStatusCode)
                {
                    response = tokenResult.ToString();
                }
            }          
            return response;
        }

        public static string AddLocation(string LocationName, string User)
        {
            string result = null;           
            var request = new
            {
                locationName = LocationName,
                userId = User
            };           
            return result;
        }

        public static string AddRoom(string locationId, string roomName, string roomAddress, string capacity, string genderSpecific, string user)
        {
           // string url = "http://localhost:51318/api/AddRoom";
            string result = null;

            var request = new
            {
                locationId = locationId,
                roomName = roomName,
                roomAddress = roomAddress,
                capacity = capacity,
                genderSpecific = genderSpecific,
                user = user
            };

            result = SetInfromationFromToAPI("AddRoom", request);
            
            return result;
        }


        public static string AddRoomBooking(string roomID, string guestName, string age, string sex, DateTime bookStartDate, DateTime bookEndDate, string user)
        {   
            string result = null;

            var request = new
            {
                roomID = roomID,
                guestName = guestName,
                age = age,
                sex = sex,
                bookStartDate = bookStartDate,
                bookEndDate = bookEndDate,
                user = user
            };

            result = SetInfromationFromToAPI("AddRoomBooking", request);

            return result;
        }
       
    }
}
