﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public interface IAppPrincipal
    {

        IAppIdentity Identity { get; set; }
    }
}
