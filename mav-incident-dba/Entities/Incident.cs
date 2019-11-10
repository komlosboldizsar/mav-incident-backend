using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba.Entities
{

    [Table("incidents")]
    public class Incident
    {

        [Column("incident_id")]
        public int ID { get; set; }

        [Column("incident_title")]
        public string Name { get; set; }

        [Column("incident_description")]
        public string Description { get; set; }

        [Column("incident_timestamp")]
        public int Timestamp { get; set; }

        [Column("incident_url")]
        public string URL { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Location> Locations { get; set; }

    }

}
