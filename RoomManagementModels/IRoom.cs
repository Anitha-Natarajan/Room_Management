using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagementModels
{
    /// <summary>
    /// Room Interface
    /// </summary>
    public interface IRoom
    {
        bool AddRoom(RoomModel roomDetails);
        bool AddLocation(LocationModel location);
        bool AddRoomBookingDetails(RoomBookingModel roomBookingDetails);
    }
}
