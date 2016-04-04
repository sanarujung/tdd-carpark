using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Models
{
    public class Ticket
    {
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string PlateNo { get; set; }
        public decimal ParkingFee {
            get
            {
                if (DateOut == null)
                    return 0;
                TimeSpan TimeFee = (DateTime)DateOut - DateIn;
                TimeFee = TimeFee.Add(TimeSpan.FromMinutes(-15));
                if (TimeFee.TotalMinutes <= 0)
                    return 0;
                else if (TimeFee.TotalHours <= 3.0)
                    return 50;
                else
                    return 50 + ((decimal)Math.Ceiling(TimeFee.TotalHours - 3) * 30);
            }
        }

    }
}
