using RoomManagementDataAccess;

namespace RoomManagement.BusinessLayer
{
    /// <summary>
    /// Business layer user 
    /// </summary>
    public class UserBL
    {
        #region Methods

        /// <summary>
        /// Get the Authentication and user role details
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetUserRole(string username, string password)
        {
            UserDL user = new UserDL();
            return user.GetUserRole(username, password);
        }
        #endregion
    }
}
