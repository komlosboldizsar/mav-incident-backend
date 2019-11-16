using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba.Entities
{

    [Table("category_filters")]
    public class CategoryFilter
    {

        [Column("criteria_id")]
        public int ID { get; set; }

        [Column("category_id")]
        public int? CategoryID { get; set; }

        public Category Category { get; set; }

        [Column("criteria_type")]
        public string TypeString { get; set; }

        [NotMapped]
        public CriteriaType Type
        {
            get { return ((TypeString == "exclude") ? CriteriaType.Exclude : CriteriaType.Include); }
        }

        [Column("criteria_wordlimit")]
        public int Wordlimit { get; set; }

        [Column("criteria_words")]
        public string WordArray { get; set; }

        [NotMapped]
        public string[] Words
        {
            get => WordArray.Split(';');
        }

    }

}
