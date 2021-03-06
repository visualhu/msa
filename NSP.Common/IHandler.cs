﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common
{
    public interface IHandler<in TMessage>
    {
        Task HandleAsync(TMessage message);
    }
}
