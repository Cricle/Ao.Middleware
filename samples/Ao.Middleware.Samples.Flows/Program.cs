using Ao.Middleware.DataWash;

namespace Ao.Middleware.Samples.Flows
{
    internal class AnyWashContext : WashContext<string>
    {
        public AnyWashContext()
        {
        }

        public AnyWashContext(IEnumerable<IDataProvider<string, object>> collection) : base(collection)
        {
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new WashBuilder<AnyWashContext,string>();
            builder.WithDelegate(x =>
            {
                x.MapData["a1"] = 123;
            }).WithDelegate(x =>
            {
                x.Outputs.Add(new DefaultColumnOutput<string, object>("a1", x["a1"]));
            });

            var handler=builder.Build();
            using (var ctx=new AnyWashContext())
            {
                await handler(ctx);
                foreach (var item in ctx.Outputs)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}