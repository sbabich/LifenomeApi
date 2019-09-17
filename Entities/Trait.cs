using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class Trait : TraitBase
    { 
        [DataMember(Name = "description", Order = 2)]
        public String Description { get; set; }

        [DataMember(Name = "connotation", Order = 3)]
        public String Connotation { get; set; }
    }
}
