using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clothesy.Domain.Entities
{
    public partial class Trip
    {
        public Trip()
        {
            Suitcase = new HashSet<Suitcase>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public DateTime StartTrip { get; set; }
        public DateTime EndTrip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }

        public virtual ICollection<Suitcase> Suitcase { get; set; }
    }
}
