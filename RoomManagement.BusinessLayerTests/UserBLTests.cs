using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomManagement.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.BusinessLayer.Tests
{
    [TestClass()]
    public class UserBLTests
    {
        [TestMethod()]
        public void GetUserRoleTest()
        {
            UserBL user = new UserBL();
            string result = user.GetUserRole("RMUser1", "RMPwd1");

            Assert.AreEqual(true, ((result.Count() > 0)));
        }
    }
}