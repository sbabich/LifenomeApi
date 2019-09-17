using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class RecommendsSkinProductsSurvey : TagBase
    {
        [DataMember(Name = "results", Order = 3)]
        public List<ProductDetail> Results { get; set; }
    }
}
