using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class OnderzoeksdomeinMapper : EntityTypeConfiguration<Onderzoeksdomein>
    {
        public OnderzoeksdomeinMapper()
        {
            ToTable("Onderzoeksdomeinen");
            HasKey(f => f.OnderzoeksdomeinId);
            Property(f => f.OnderzoeksdomeinId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.Naam).IsOptional();

            
        }
    }
}