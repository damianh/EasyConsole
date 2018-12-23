using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace Demo.Pages
{
    class Page1B : Page
    {
        public Page1B(Program program)
            : base("Page 1B", program)
        {
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            Output.WriteLine("Hello from Page 1B");

            Input.ReadString("Press [Enter] to navigate home");
            await Program.NavigateHome(cancellationToken);
        }
    }
}
