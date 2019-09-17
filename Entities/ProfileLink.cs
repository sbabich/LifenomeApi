using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    [Serializable()]
    public class ProfileLink
    {
        [DataMember(Name = "href", EmitDefaultValue = false, IsRequired = true, Order = 0)]
        public String Href { get; set; }

        [DataMember(Name = "rel", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public String Rel { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", Href, Rel);
        }
    }
}
