using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class RecommendGroceries
    {
        [DataMember(Name = "score", Order = 0)]
        public Single Score { get; set; }

        [DataMember(Name = "traits", Order = 1)]
        public List<TraitBase> Traits { get; set; }

        [DataMember(Name = "data", Order = 2)]
        public RecommendGroceriesData Data { get; set; }

        [DataContract()]
        public class RecommendGroceriesData
        {
            [DataMember(Name = "name", Order = 0)]
            public String Name { get; set; }

            [DataMember(Name = "category", Order = 1)]
            public Category Category { get; set; }
        }
    }
}
