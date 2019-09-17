using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    public class RecommendsRecipes
    {
        [DataMember(Name = "score", Order = 0)]
        public Single Score { get; set; }

        [DataMember(Name = "traits", Order = 1)]
        public List<TraitBase> Traits { get; set; }

        [DataMember(Name = "data", Order = 2)]
        public RecommendsRecipesData Data { get; set; }

        [DataContract()]
        public class RecommendsRecipesData
        {
            [DataMember(Name = "nutrition", Order = 0)]
            public Nutrition Nutrition { get; set; }

            [DataMember(Name = "ingredients", Order = 1)]
            public List<String> Ingredients { get; set; }

            [DataMember(Name = "url", Order = 2)]
            public String Url { get; set; }

            [DataMember(Name = "image", Order = 3)]
            public String ImageUrl { get; set; }

            [DataMember(Name = "title", Order = 4)]
            public String Title { get; set; }

            [DataMember(Name = "calories",  Order = 5)]
            public Single Calories { get; set; }

            [DataMember(Name = "servings",  Order = 6)]
            public Single Servings { get; set; }

            [DataMember(Name = "tags", Order = 7)]
            public List<String> Tags { get; set; }
        }
    }
}
