﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDestroyer.Model
{
    public interface IPayoff
    {
        IEnumerable<string> Generate();
    }
}
