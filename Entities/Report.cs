using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class Report
    {
        [DataMember(Name = "code", Order = 0)]
        public String Code { get; set; }

        [DataMember(Name = "name", Order = 1)]
        public String Name { get; set; }

        [DataMember(Name = "description", Order = 2)]
        public String Description { get; set; }

        [DataMember(Name = "sections", Order = 3)]
        public List<ReportSection> Sections { get; set; }

        [DataMember(Name = "links", Order = 4)]
        public List<ProfileLink> Links { get; set; }
    }
}
