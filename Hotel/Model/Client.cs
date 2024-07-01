using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    using System.Collections.Generic;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Reservation = new HashSet<Reservation>();
        }

        public int ClientID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public System.DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int PassportSeries { get; set; }
        public int PassportNumber { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
