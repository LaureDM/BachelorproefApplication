using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using MySql.Data.Entity;
using p2g21.Models.DAL.Mapper;
using p2g21.Models.Domain;

namespace p2g21.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BachelorProefContext : DbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Onderzoeksdomein> Onderzoeksdomeinen { get; set; }
        
        public BachelorProefContext() : base("BachelorProef")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VoorstelMapper());
            modelBuilder.Configurations.Add(new FeedbackMapper());
            modelBuilder.Configurations.Add(new GebruikerMapper());
            modelBuilder.Configurations.Add(new OnderzoeksdomeinMapper());
            modelBuilder.Configurations.Add(new DossierMapper());
            modelBuilder.Configurations.Add(new HistoriekMapper());
        }
    }
}