using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public class Option
    {
        public string Name { get; }

        public Func<CancellationToken, Task> Callback { get; }

        public Option(string name, Func<CancellationToken, Task> callback)
        {
            Name = name;
            Callback = callback;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
