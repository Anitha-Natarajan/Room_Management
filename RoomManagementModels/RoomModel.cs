using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementModels
{    
    public class RoomModel
    {
        private int locationID;
        private string locationName;
        private int roomID;
        private string roomName;
        private string roomAddress;
        private int capacity;
        private string availableStatus;
        private string isGenderSpecific;
        private bool isSharedRoom;
        private bool isActive;
        private string createdBy;
        private DateTime createdTime;
        private string modifiedBy;
        private DateTime modifiedDate;


        public int LocationID { get => locationID; set => locationID = value; }
        public string LocationName { get => locationName; set => locationName = value; }

        public int RoomID { get => roomID; set => roomID = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public string RoomAddress { get => roomAddress; set => roomAddress = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public string AvailableStatus { get => availableStatus; set => availableStatus = value; }
        public string IsGenderSpecific { get => isGenderSpecific; set => isGenderSpecific = value; }
        public bool IsSharedRoom { get => isSharedRoom; set => isSharedRoom = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string ModifiedBy { get => modifiedBy; set => modifiedBy = value; }
        public DateTime ModifiedDate { get => modifiedDate; set => modifiedDate = value; }

        public RoomModel()
        {
        }

        public RoomModel(int roomId, string roomName, string gender)
        {            
            this.roomID = roomId;
            this.roomName = roomName;
            this.IsGenderSpecific = gender;
        }

        public RoomModel(int locationId, string locationName, int roomId, string roomName, string roomAddress, int capacity, string isGenderSpecific, bool isSharedRoom, string user)
        {
            this.locationID = locationId;
            this.LocationName = locationName;
            this.roomID = roomId;
            this.roomName = roomName;
            this.roomAddress = RoomAddress;
            this.capacity = capacity;
            this.isGenderSpecific = isGenderSpecific;
            this.isSharedRoom = IsSharedRoom;
            this.isActive = true;
            this.createdBy = user;
            this.createdTime = DateTime.Now;
            this.modifiedBy = user;
            this.modifiedDate = DateTime.Now;

        }

       
    }
}
