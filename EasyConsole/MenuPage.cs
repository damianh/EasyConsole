using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public abstract class MenuPage : Page
    {
        protected Menu Menu { get; set; }

        public MenuPage(string title, Program program, params Option[] options)
            : base(title, program)
        {
            Menu = new Menu();

            foreach (var option in options)
                Menu.Add(option);
        }

        public override async Task Display(CancellationToken cancellationToken)
        {
            await base.Display(cancellationToken);

            if (Program.NavigationEnabled && !Menu.Contains("Go back"))
            {
                Menu.AddAsync("Go back", async ct => await Program.NavigateBack(ct));
            }

            await Menu.Display(cancellationToken);
        }
    }
}
