using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class HistoriekMapper : EntityTypeConfiguration<Historiek>
    {
        public HistoriekMapper()
        {
            ToTable("Historieken");
            HasKey(m => m.HistoriekId);
            Property(m => m.HistoriekId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.VolledigeLog).IsRequired();
        }
    }
}