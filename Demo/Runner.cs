using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    class Runner
    {
        static Task Main(string[] args)
        {
            return new DemoProgram().Run(CancellationToken.None);
        }
    }
}
