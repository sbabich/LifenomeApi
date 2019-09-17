using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Exceptions
{
    public class APIInvalidValueException : APIException
    {
        public APIInvalidValueException(String message)
            : base(message)
        { }

        public APIInvalidValueException(String message, Object o1)
            : base(String.Format(message, o1))
        { }

        public APIInvalidValueException(String message, Object o1, Object o2)
            : base(String.Format(message, o1, o2))
        { }

        public APIInvalidValueException(String message, Object o1, Object o2, Object o3)
            : base(String.Format(message, o1, o2, o3))
        { }
    }
}
