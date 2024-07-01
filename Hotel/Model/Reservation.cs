using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Reservation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reservation()
        {
            this.ReservationTariff = new HashSet<ReservationTariff>();
        }

        public int ReservationID { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public System.DateTime CheckiInDate { get; set; }
        public System.DateTime CheckOutDate { get; set; }
        public int FullCost { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> RoomID { get; set; }

        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationTariff> ReservationTariff { get; set; }
        public virtual Room Room { get; set; }
    }
}
