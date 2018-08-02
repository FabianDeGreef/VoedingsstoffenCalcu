using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoedingsstoffenCalcu.DomainClasses
{
    public class DayEntry
    {
        public DayEntry()
        {
            SavedProducts = new List<SavedProduct>();
        }
        public int DayEntryId { get; set; }
        public Result Result { get; set; }
        public DateTime CurentDate { get; set; }
        public List<SavedProduct> SavedProducts { get; set; }
    }
}
