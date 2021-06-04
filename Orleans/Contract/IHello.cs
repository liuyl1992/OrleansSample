using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
