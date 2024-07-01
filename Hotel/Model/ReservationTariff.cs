using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    using System;
    using System.Collections.Generic;

    public partial class ReservationTariff
    {
        public int ReservationTariffID { get; set; }
        public Nullable<int> ReservationID { get; set; }
        public Nullable<int> TariffID { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Tariff Tariff { get; set; }
    }
}
