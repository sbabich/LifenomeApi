using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Entities
{
    [DataContract()]
    [Serializable()]
    public class Profile : ICloneable
    {
        [DataMember(Name = "id", EmitDefaultValue = false, IsRequired = false, Order = 0)]
        public int Id { get; set; }

        [DataMember(Name = "first_name", IsRequired = false, Order = 1)]
        public String FirstName { get; set; }

        [DataMember(Name = "last_name", IsRequired = false, Order = 2)]
        public String LastName { get; set; }

        [DataMember(Name = "gender", IsRequired = false, Order = 3)]
        public String Gender { get; set; }

        [DataMember(Name = "date_of_birth", IsRequired = false, Order = 4)]
        public String DateOfBirth { get; set; }

        [DataMember(Name = "ethnicity", EmitDefaultValue = false, IsRequired = false, Order = 5)]
        public String Ethnicity { get; set; }

        [DataMember(Name = "genotype", EmitDefaultValue = false, IsRequired = false, Order = 6)]
        public Genotype Genotype { get; set; }
        
        [DataMember(Name = "links", EmitDefaultValue = false, IsRequired = false, Order = 7)]
        public List<ProfileLink> Links { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Id, FirstName, LastName);
        }

        public object Clone()
        {
            using (MemoryStream stm = new MemoryStream())
            {
                BinaryFormatter ibf = new BinaryFormatter();

                ibf.Serialize(stm, this);

                stm.Flush();
                stm.Position = 0;

                return (Profile)ibf.Deserialize(stm);
            }
        }
    }
}
