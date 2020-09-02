using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace RoomManagementDataAccess
{
    public class UserDL
    {
        string connectionString = null;
        public UserDL()
        {
            try
            {

                connectionString = @"Data Source=np:\\.\pipe\LOCALDB#C0942EDA\tsql\query;Initial Catalog=RoomManagement;Integrated Security=True";
                // connectionString = ConfigurationManager.ConnectionStrings["RoomManagement"].ConnectionString;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Get Location details from Database
        /// </summary>
        /// <returns></returns>
        public string GetUserRole(string username, string password)
        {
            string userRole = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("sp_UserAuthentication", sqlConnection);
            SqlDataReader reader = null;
            try
            {

                sqlCommand.Parameters.Add(new SqlParameter("@userName", username));
                sqlCommand.Parameters.Add(new SqlParameter("@userPassword", password));
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    userRole = reader["UserRole"].ToString();                    
                }

                // Close the reader and the connection
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                
            }
            finally
            {
                reader.Close();
                sqlConnection.Close();
            }
            return userRole;
        }
    }
}
