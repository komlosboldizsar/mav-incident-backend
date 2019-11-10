using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba.Entities
{

    [Table("categories")]
    public class Category
    {

        [Column("category_id")]
        public int ID { get; set; }

        [Column("category_name")]
        public string Name { get; set; }

        [Column("category_description")]
        public string Description { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }

        [ForeignKey(nameof(CategoryFilter.CategoryID))]
        public virtual ICollection<CategoryFilter> Filters { get; set; }

    }

}
