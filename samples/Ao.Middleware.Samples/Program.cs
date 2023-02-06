namespace Ao.Middleware.Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new MiddlewareBuilder<Sum>();
            for (int i = 0; i < 10; i++)
            {
                builder.Use(x => x.Count++);
            }
            var handler = builder.Build();
            var s = new Sum();
            handler(s).GetAwaiter().GetResult();
            Console.WriteLine(s.Count);//10
        }
    }
    public class Sum
    {
        public int Count { get; set; }
    }
}