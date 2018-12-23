using EasyConsole;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Pages
{
    class InputPage : Page
    {
        public InputPage(Program program)
            : base("Input", program)
        {
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            var input = await Input.ReadEnum<Fruit>("Select a fruit", cancellationToken);
            Output.WriteLine(ConsoleColor.Green, "You selected {0}", input);

            Input.ReadString("Press [Enter] to navigate home");
            await Program.NavigateHome(cancellationToken);
        }
    }

    enum Fruit
    {
        Apple,
        Banana,
        Coconut
    }
}
