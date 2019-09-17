using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()] 
    public class APIErrorResponse : APIResponse
    {
        [DataMember(Name = "detail", IsRequired = false)]
        public String Detail { get; set; }
    }
}
