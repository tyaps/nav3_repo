using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rovnicaMVC.Domain
{
    public class Formatting
    {

        public static string hq_need_dueDate(DateTime? duedate)
        {
            if (!duedate.HasValue)
                return "--";
            else
            {
                int dayDifference = DateTime.Now.Date.Subtract(duedate.Value.Date).Days;
                if (dayDifference == 0)
                    return "Сегодня " + duedate.Value.ToString("HH:mm");
                if (dayDifference == -1)
                    return "Завтра " + duedate.Value.ToString("HH:mm");

                if (dayDifference > 1)
                    return "!! " + duedate.Value.ToString("dd.MM.yyyy HH:mm");

                return duedate.Value.ToString("dd.MM.yyyy hh:mm");
            }
        }
    }
}
