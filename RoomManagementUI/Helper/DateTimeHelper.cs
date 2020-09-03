using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomManagementUI.Helper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Combine the Date and Time Values
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="timeValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeValue(DateTimePicker dateValue, DateTimePicker timeValue)
        {
            string date = String.Format("{0:MM/dd/yyyy}", dateValue.Value);
            string time = String.Format("{0:HH:mm:ss}", timeValue.Value);
            DateTime dateTime = Convert.ToDateTime(date + " " + time);
            return dateTime;
        }
    }
}
