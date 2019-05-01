using System;
using System.Threading.Tasks;
using MassTransit;

namespace api
{
    public class TestConsumer : IConsumer<ITestEvent>
    {
        public async Task Consume (ConsumeContext<ITestEvent> context)
        {
            await Console.Out.WriteLineAsync (context.Message.Name);
        }
    }

    public interface ITestEvent
    {
        string Name { get; }
    }
}