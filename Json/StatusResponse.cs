using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()]
    public class StatusResponse : APIResponse
    {
        //{"status":"processing"}
        [DataMember(Name = "status")]
        public String Status { get; set; }
    }
}
