using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public abstract class Page
    {
        public string Title { get; }

        public Program Program { get; set; }

        public Page(string title, Program program)
        {
            Title = title;
            Program = program;
        }

        public virtual Task Display(CancellationToken cancellationToken)
        {
            if (Program.History.Count > 1 && Program.BreadcrumbHeader)
            {
                var breadcrumb = Program
                    .History
                    .Select((page) => page.Title)
                    .Reverse()
                    .Aggregate<string, string>(null, (current, title) => current + (title + " > "));
                breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);
                Console.WriteLine(breadcrumb);
            }
            else
            {
                Console.WriteLine(Title);
            }
            Console.WriteLine("---");
            return Task.CompletedTask;
        }
    }
}
