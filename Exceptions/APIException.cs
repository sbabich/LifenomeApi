﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Exceptions
{
    public class APIException : Exception
    {
        public APIException(String message, Exception inner = null)
            : base(message, inner)
        { }
    }
}
