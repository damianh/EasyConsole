using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public class Menu
    {
        private IList<Option> Options { get; set; }

        public Menu()
        {
            Options = new List<Option>();
        }

        public Task Display(CancellationToken cancellationToken)
        {
            for (var i = 0; i < Options.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
            }
            var choice = Input.ReadInt("Choose an option:", min: 1, max: Options.Count);

            return Options[choice - 1].Callback(cancellationToken);
        }

        public Menu AddSync(string option, Action callback)
        {
            Task CallbackAsync(CancellationToken _)
            {
                callback();
                return Task.CompletedTask;
            }

            return Add(new Option(option, CallbackAsync));
        }

        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

        public Menu Add(string option, Func<CancellationToken, Task> callback)
        {
            return Add(new Option(option, callback));
        }

        public bool Contains(string option)
        {
            return Options.FirstOrDefault((op) => op.Name.Equals(option)) != null;
        }
    }
}
