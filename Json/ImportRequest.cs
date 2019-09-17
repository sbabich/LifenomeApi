using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()]
    public class ImportRequest
    {
        [DataMember(Name = "access_token", EmitDefaultValue = false, IsRequired = false, Order = 0)]
        public String AccessToken { get; set; }
        [DataMember(Name = "refresh_token", EmitDefaultValue = false, IsRequired = false, Order = 1)]
        public String RefreshToken { get; set; }
        [DataMember(Name = "profile_id", EmitDefaultValue = false, IsRequired = false, Order = 2)]
        public String ProfileId { get; set; }
    }
}
