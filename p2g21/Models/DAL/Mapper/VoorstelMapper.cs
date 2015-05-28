using p2g21.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace p2g21.Models.DAL.Mapper
{
    public class VoorstelMapper : EntityTypeConfiguration<Voorstel>
    {
        public VoorstelMapper()
        {
            ToTable("Voorstel");
            HasKey(t => t.VoorstelId);
            Property(t => t.VoorstelId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.VoorstelTitel).IsOptional().HasColumnName("Titel");
            Property(t => t.ProbleemStelling).IsOptional().HasColumnName("Probleemstelling");
            Property(t => t.Context).IsOptional();
            Property(t => t.Doelstelling).IsOptional();
            Property(t => t.Onderzoeksvraag).IsOptional();
            Property(t => t.PlanVanAanpak).IsOptional().HasColumnName("Plan van aanpak");
            //HasOptional(v=>v.Feedback).WithRequired().Map(m=>m.MapKey("VoorstelID")).WillCascadeOnDelete(true);
            Property(t => t.ReferentieLijst).IsOptional();
            Property(t => t.Creatiedatum).IsOptional();
            Property(t => t.TijdstipIndienen).IsOptional();
            Property(t => t.Creatiedatum).IsOptional();
            HasMany(b => b.Onderzoeksdomeinen).WithMany(m=>m.Voorstellen).Map(m=>
            {m.MapLeftKey("V_Id");
                m.MapRightKey("O_Id");
            });
            Ignore(v => v.HuidigeToestand);
            HasRequired(v => v.Historiek).WithOptional().Map(m => m.MapKey("HistoriekID")).WillCascadeOnDelete(true);
        }
    }
}