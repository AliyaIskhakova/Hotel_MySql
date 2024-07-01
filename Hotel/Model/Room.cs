using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            this.Reservation = new HashSet<Reservation>();
        }

        public int RoomID { get; set; }
        public string Summary { get; set; }
        public float Area { get; set; }
        public int RoomQuantity { get; set; }
        public int PeopleQuantity { get; set; }
        public int Cost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
