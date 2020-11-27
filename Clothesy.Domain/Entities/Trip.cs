using System;
using System.Collections.Generic;

namespace Clothesy.Domain.Entities
{
    public partial class Trip
    {
        public Trip()
        {
            Suitcase = new HashSet<Suitcase>();
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int idTrip { get; set; }
        public string TripName { get; set; }
        public DateTime StartTrip { get; set; }
        public DateTime EndTrip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int idUser { get; set; }
        public virtual User idUserNavigation { get; set; }

        public virtual ICollection<Suitcase> Suitcase { get; set; }
    }
}
