using System;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Runtime;

namespace Client
{
    public class HelloWorldClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public HelloWorldClientHostedService(IClusterClient client)
        {
            this._client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling grains from the initialized client
            var friend = _client.GetGrain<IHello>(0);
            var friend0 = _client.GetGrain<IHello>(0);
            var friend1 = _client.GetGrain<IHello>(1);


            var response = await friend.SayHello("刘奕良");
            
            var response1 = await friend.SayHello("刘奕良!----------------1");
            var response0 = await friend.SayHello("刘奕良!----------------0");
            Console.WriteLine($"{response}");

            // example of calling IHelloArchive grqain that implements persistence
            var g = this._client.GetGrain<IHelloArchive>(0);
            response = await g.SayHello("Good day!");
            Console.WriteLine($"{response}");

            response = await g.SayHello("Good evening!");
            Console.WriteLine($"{response}");

            var greetings = await g.GetGreetings();
            Console.WriteLine($"\nArchived greetings: {Utils.EnumerableToString(greetings)}");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}
