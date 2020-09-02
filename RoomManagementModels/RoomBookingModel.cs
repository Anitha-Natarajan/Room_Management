using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementModels
{
    public class RoomBookingModel
    {
        private int roomID;
        private string roomName;
        private string guestName;
        private string sex;
        private int age;
        
        private DateTime bookingStartDate;
        private DateTime bookingEndDate;
        private bool isActive;
        private string createdBy;
        private DateTime createdTime;
        private string modifiedBy;
        private DateTime modifiedDate;

        public int RoomID { get => roomID; set => roomID = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public string GuestName { get => guestName; set => guestName = value; }     
        public string Sex { get => sex; set => sex = value; }
        public int Age { get => age; set => age = value; }
        public DateTime BookingStartDate { get => bookingStartDate; set => bookingStartDate = value; }
        public DateTime BookingEndDate { get => bookingEndDate; set => bookingEndDate = value; }

        public bool IsActive { get => isActive; set => isActive = value; }

        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string ModifiedBy { get => modifiedBy; set => modifiedBy = value; }
        public DateTime ModifiedDate { get => modifiedDate; set => modifiedDate = value; }

        #region Constructor
        public RoomBookingModel()
        { }
        public RoomBookingModel(int roomId, string roomName, string guestName, string sex, int age, 
                                  DateTime bookingStartDate, DateTime bookingEndDate, string user)
        {
            
            this.roomID = roomId;
            this.roomName = roomName;
            this.guestName = guestName;
            this.age = age;
            this.sex = sex;

            this.bookingStartDate = bookingStartDate;
            this.bookingEndDate = bookingEndDate;

            this.isActive = true;
            this.createdBy = user;
            this.createdTime = DateTime.Now;
            this.modifiedBy = user;
            this.modifiedDate = DateTime.Now;

        }
        #endregion

    }
}
