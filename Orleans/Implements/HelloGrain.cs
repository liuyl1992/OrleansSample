using Contract;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements
{
    /// <summary>
    /// Orleans grain implementation class HelloGrain.
    /// </summary>
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly IPersistentState<ProfileState> _profile;
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger,
            [PersistentState("profile", "profileStore")] IPersistentState<ProfileState> profile)
        {
            this.logger = logger;
            _profile = profile;
        }

        Task<string> IHello.SayHello(string greeting)
        {
            IHelloArchive player = GrainFactory.GetGrain<IHelloArchive>(0);
            _profile.State.Name = "greeting";
            logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello!");
        }
    }

    [Serializable]
    public class ProfileState
    {
        public string Name { get; set; }

    }
}
