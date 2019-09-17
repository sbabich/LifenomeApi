using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class TagBase
    {
        [DataMember(Name = "count", Order = 0)]
        public int Count { get; set; }

        [DataMember(Name = "next", Order = 1)]
        public String Next { get; set; }

        [DataMember(Name = "previous", Order = 2)]
        public String Previous { get; set; }
    }
}
