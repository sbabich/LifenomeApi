using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class Product
    {
        [DataMember(Name = "category", Order = 0)]
        public Category Category { get; set; }

        [DataMember(Name = "rating", Order = 1)]
        public Single Rating { get; set; }

        [DataMember(Name = "description", Order = 2)]
        public String Description { get; set; }

        [DataMember(Name = "url", Order = 3)]
        public String Url { get; set; }

        [DataMember(Name = "brand", Order = 4)]
        public String Brand { get; set; }

        [DataMember(Name = "price", Order = 5)]
        public Single Price { get; set; }

        [DataMember(Name = "num_reviews", Order = 6)]
        public Single NumReviews { get; set; }

        [DataMember(Name = "image_url", Order = 7)]
        public String ImageUrl { get; set; }

        [DataMember(Name = "id", Order = 8)]
        public String Id { get; set; }

        [DataMember(Name = "name", Order = 9)]
        public String Name { get; set; }
    }
}
