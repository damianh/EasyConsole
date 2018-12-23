using System.Threading;
using System.Threading.Tasks;
using EasyConsole;

namespace Demo.Pages
{
    class Page1Ai : Page
    {
        public Page1Ai(Program program)
            : base("Page 1Ai", program)
        {
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            Output.WriteLine("Hello from Page 1Ai");

            Input.ReadString("Press [Enter] to navigate home");
            await Program.NavigateHome(cancellationToken);
        }
    }
}
