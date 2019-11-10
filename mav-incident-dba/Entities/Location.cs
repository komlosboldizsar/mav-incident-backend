using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba.Entities
{

    [Table("locations")]
    public class Location
    {

        [Column("location_id")]
        public int ID { get; set; }

        [Column("location_name")]
        public string Name { get; set; }

        [Column("location_coord_lat")]
        public decimal Latitude { get; set; }

        [Column("location_coord_lon")]
        public decimal Longitude { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

    }

}
