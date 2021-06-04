﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IHelloArchive : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);

        Task<IEnumerable<string>> GetGreetings();
    }
}
