using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace Demo.Pages
{
    class Page2 : Page
    {
        public Page2(Program program)
            : base("Page 2", program)
        {
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            Output.WriteLine("Hello from Page 2");

            Input.ReadString("Press [Enter] to navigate home");
            await Program.NavigateHome(cancellationToken);
        }
    }
}
