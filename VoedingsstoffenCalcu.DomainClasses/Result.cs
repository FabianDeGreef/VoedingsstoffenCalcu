using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class Result
    {
        public int ResultId { get; set; }

        public DayEntry DayEntry { get; set; }

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
    }
}
