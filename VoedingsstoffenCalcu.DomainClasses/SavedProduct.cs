using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class SavedProduct
    {
        public int SavedProductId { get; set; }

        public int OrginalProductId { get; set; }

        public int? DayEntryId { get; set; }

        public string Naam { get; set; }

        public decimal Kilocalorieën { get; set; }

        public decimal Kilojoules { get; set; }

        public decimal Water { get; set; }

        public decimal Eiwit { get; set; }

        public decimal Koolhydraten { get; set; }

        public decimal Suikers { get; set; }

        public decimal Vet { get; set; }

        public decimal VerzadigdVet { get; set; }

        public decimal EnkelvoudigOnverzadigdVet { get; set; }

        public decimal MeervoudigOnverzadigdeVetzuren { get; set; }

        public decimal Cholesterol { get; set; }

        public decimal Vezels { get; set; }

        public decimal Gewicht { get; set; }

    }
}
