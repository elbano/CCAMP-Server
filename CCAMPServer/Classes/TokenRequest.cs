﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class TokenRequest
    {
        public string EmailAddress { get; set; }

        public string RedirectUri { get; set; }
    }
}
