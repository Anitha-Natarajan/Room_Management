using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomManagement.BusinessLayer;
using RoomManagementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.BusinessLayer.Tests
{
    [TestClass()]
    public class RoomTests
    {
        [TestMethod()]
        public void AddLocationTest()
        {
            RoomBL room = new RoomBL();
            LocationModel location = new LocationModel();
            location.LocationName = "Madurai";
            location.CreatedBy = "anitha";
            bool result = room.AddLocation(location);
            Assert.AreEqual(true, result);
        }

        [TestMethod()]
        public void GetLocationTest()
        {
            List<LocationModel> result = null;
            RoomBL room = new RoomBL();
            result = room.GetLocation();

            Assert.AreEqual(true, ((result.Count() > 0)));
        }
    }
}