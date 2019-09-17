using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class ReportSectionEntry
    {
        [DataMember(Name = "trait", Order = 0)]
        public Trait Trait { get; set; }

        [DataMember(Name = "percentile", Order = 1)]
        public int Percentile { get; set; }

        [DataMember(Name = "assessment", Order = 2)]
        public int Assessment { get; set; }

        [DataMember(Name = "assessment_recommendation", Order = 3)]
        public String Recommendation { get; set; }
        
        [DataMember(Name = "snps_contributing", Order = 4)]
        public List<String> Contributing { get; set; }

        [DataMember(Name = "snps_inhibiting", Order = 5)]
        public List<String> Inhibiting { get; set; }
    }
}
