using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementModels
{

    /// <summary>
    /// Location Class
    /// </summary>
    public class LocationModel
    {       

        private int locationId;
        private string locationName;
        private bool isActive;
        private string createdBy;
        private DateTime createdTime;
        private string modifiedBy;
        private DateTime modifiedDate;

        public int LocationId { get => locationId; set => locationId = value; }
        public string LocationName { get => locationName; set => locationName = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string CreatedBy { get => createdBy; set => createdBy = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string ModifiedBy { get => modifiedBy; set => modifiedBy = value; }
        public DateTime ModifiedDate { get => modifiedDate; set => modifiedDate = value; }

        #region Constructor
        public LocationModel()
        { }

        public LocationModel(int locationId, string locationName)
        {
            this.locationId = locationId;
            this.locationName = locationName;
        }
        #endregion
    }
}
