using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class Nutrition
    {
        [DataMember(Name = "Carbs", Order = 0)]
        public List<String> Carbs { get; set; }

        [DataMember(Name = "Fiber", Order = 1)]
        public List<String> Fiber { get; set; }

        [DataMember(Name = "Saturated", Order = 2)]
        public List<String> Saturated { get; set; }

        [DataMember(Name = "Sodium", Order = 3)]
        public List<String> Sodium { get; set; }

        [DataMember(Name = "Energy", Order = 4)]
        public List<String> Energy { get; set; }

        [DataMember(Name = "Sugars", Order = 5)]
        public List<String> Sugars { get; set; }

        [DataMember(Name = "Fat", Order = 6)]
        public List<String> Fat { get; set; }

        [DataMember(Name = "Cholesterol", Order = 7)]
        public List<String> Cholesterol { get; set; }

        [DataMember(Name = "Protein", Order = 8)]
        public List<String> Protein { get; set; }
    }
}
