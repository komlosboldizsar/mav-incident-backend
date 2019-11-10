using mav_incident_dba.Entities;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mav_incident_dba
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class IncidentContext : DbContext
    {
        
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public IncidentContext()
            : base()
        { }

        public IncidentContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // @source https://stackoverflow.com/questions/12237617/entityframework-same-table-many-to-many-relationship
            modelBuilder.Entity<Incident>()
                .HasMany<Category>(i => i.Categories)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("incident_id");
                    m.MapRightKey("category_id");
                    m.ToTable("incident_categories");
                });

            modelBuilder.Entity<Incident>()
                .HasMany<Location>(i => i.Locations)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("incident_id");
                    m.MapRightKey("location_id");
                    m.ToTable("incident_locations");
                });

        }

    }

}
