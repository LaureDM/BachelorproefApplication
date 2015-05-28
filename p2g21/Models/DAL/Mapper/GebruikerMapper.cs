using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class GebruikerMapper : EntityTypeConfiguration<Gebruiker>
    {
        public GebruikerMapper()
        {
            ToTable("Gebruiker");
            HasKey(t => t.GebruikerId);
            Property(t => t.GebruikerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Email).IsRequired();
            Property(t => t.Loginnaam).IsRequired().HasMaxLength(50);
            Property(t => t.Naam).IsRequired().HasMaxLength(50);
            Property(t => t.Voornaam).IsRequired().HasMaxLength(50);
            Property(t => t.Wachtwoord).IsRequired();
            Property(t => t.EersteGebruik);

        }
    }
}