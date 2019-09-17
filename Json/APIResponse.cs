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
    public class APIResponse
    {
        public static T Parse<T>(String json)
        {
            try
            {
                APIErrorResponse resp_e = JsonHelper.Parse<APIErrorResponse>(json);
                throw new APIException(String.Format("Error: {0} ", resp_e.Detail));
            }
            catch (Exception ex)
            { }

            return JsonHelper.Parse<T>(json);
        }

        public virtual void CheckDataAndThrowIfHasError()
        {
        }
    }
}
