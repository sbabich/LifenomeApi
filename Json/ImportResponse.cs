using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()]
    public class ImportResponse : APIResponse
    {
        [DataMember(Name = "msg")]
        public String Message { get; set; }
    }
}
