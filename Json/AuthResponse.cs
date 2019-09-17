using LifenomeAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()]
    public class AuthResponse : APIResponse
    {
        [DataMember(Name = "access_token", IsRequired = false)]
        public String AccessToken { get; set; }
        
        [DataMember(Name = "token_type", IsRequired = false)]
        public String TypeToken { get; set; }

        [DataMember(Name = "expires_in", IsRequired = false)]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "scope", IsRequired = false)]
        public String Scope { get; set; }

        public override void CheckDataAndThrowIfHasError()
        {
            base.CheckDataAndThrowIfHasError();

            if (String.IsNullOrEmpty(Scope))
                throw new APIException("The scope is empty!");
        }
    }
}
