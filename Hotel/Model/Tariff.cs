using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    using System;
    using System.Collections.Generic;

    public partial class Tariff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tariff()
        {
            this.ReservationTariff = new HashSet<ReservationTariff>();
        }

        public int TariffID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public bool Food { get; set; }
        public bool Gym { get; set; }
        public bool Transfer { get; set; }
        public bool Wifi { get; set; }
        public int Cost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservationTariff> ReservationTariff { get; set; }
    }
}
