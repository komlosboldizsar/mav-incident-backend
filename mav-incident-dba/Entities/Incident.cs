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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("incident_id")]
        public int ID { get; set; }

        [Column("incident_title")]
        public string Name { get; set; }

        [Column("incident_description")]
        public string Description { get; set; }

        [Column("incident_creation_timestamp")]
        public int CreationTimestamp { get; set; }

        [Column("incident_update_timestamp")]
        public int UpdateTimestamp { get; set; }

        [Column("incident_process_timestamp")]
        public int ProcessTimestamp { get; set; }

        [Column("incident_hash")]
        public string Hash { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

    }

}
