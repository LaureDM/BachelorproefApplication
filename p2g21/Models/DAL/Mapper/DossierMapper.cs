using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class DossierMapper : EntityTypeConfiguration<Dossier>
    {
        public DossierMapper()
        {
            ToTable("Dossiers");
            Property(f => f.DossierId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(v=>v.IngediendVoorstel).WithOptional().Map(m=>m.MapKey("DossierVoorstelID")).WillCascadeOnDelete(true);
            Property(f => f.Geaccepteerd).IsOptional();
            Property(l => l.StudentId).IsRequired();

        }
    }
}