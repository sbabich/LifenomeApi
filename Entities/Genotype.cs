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
    public class Genotype
    {
        [DataMember(Name = "format", EmitDefaultValue = false, Order = 0)]
        public String Format { get; set; }

        [DataMember(Name = "fingerprint", EmitDefaultValue = false, Order = 1)]
        public String FingerPrint { get; set; }

        [DataMember(Name = "genotype_file", EmitDefaultValue = false, Order = 2)]
        public String GenotypeFile { get; set; }
    }
}