using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementModels
{
    public class UserModel
    {
        private string userName;
        private string userRole;
        private string userPasswrod;
        private bool isActive;
        private string createdBy;
        private DateTime createdTime;
        private string modifiedBy;
        private DateTime modifiedDate;

        public string UserName { get => userName; set => userName = value; }
        public string UserRole { get => userRole; set => userRole = value; }
        public string UserPasswrod { get => userPasswrod; set => userPasswrod = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string ModifiedBy { get => modifiedBy; set => modifiedBy = value; }
        public DateTime ModifiedDate { get => modifiedDate; set => modifiedDate = value; }
        
    }
}
