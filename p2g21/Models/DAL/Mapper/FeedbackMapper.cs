using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class FeedbackMapper: EntityTypeConfiguration<Feedback>
    {
        public FeedbackMapper()
        {
            ToTable("Feedback");
            HasKey(f => f.FeedbackID);
            Property(f => f.FeedbackID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.Bijdrage).IsRequired();
            Property(f => f.Bron).IsRequired();
            Property(f => f.Context).IsRequired();
            Property(f => f.Doelstellingen).IsRequired();
            Property(f => f.Onderwerp).IsRequired();
            Property(f => f.Onderzoeksvraag).IsRequired();
            Property(f => f.Suggesties).IsOptional();
        }
    }
}