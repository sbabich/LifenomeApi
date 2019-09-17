using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class TraitBase
    {
        [DataMember(Name = "code", Order = 0)]
        public String Code { get; set; }

        [DataMember(Name = "name", Order = 1)]
        public String Name { get; set; }
    }
}
