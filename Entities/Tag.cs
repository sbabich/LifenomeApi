using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class Tag
    {
        [DataMember(Name = "name", Order = 0)]
        public String Name { get; set; }
    }
}
