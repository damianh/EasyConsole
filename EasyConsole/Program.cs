using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyConsole
{
    public abstract class Program
    {
        protected string Title { get; set; }

        public bool BreadcrumbHeader { get; }

        protected Page CurrentPage => (History.Any()) ? History.Peek() : null;

        private Dictionary<Type, Page> Pages { get; }

        public Stack<Page> History { get; }

        public bool NavigationEnabled => History.Count > 1;

        protected Program(string title, bool breadcrumbHeader)
        {
            Title = title;
            Pages = new Dictionary<Type, Page>();
            History = new Stack<Page>();
            BreadcrumbHeader = breadcrumbHeader;
        }

        public virtual async Task Run(CancellationToken cancellationToken)
        {
            try
            {
                Console.Title = Title;

                await CurrentPage.Display(cancellationToken);
            }
            catch (Exception e)
            {
                Output.WriteLine(ConsoleColor.Red, e.ToString());
            }
            finally
            {
                if (Debugger.IsAttached)
                {
                    Input.ReadString("Press [Enter] to exit");
                }
            }
        }

        public void AddPage(Page page)
        {
            var pageType = page.GetType();

            if (Pages.ContainsKey(pageType))
                Pages[pageType] = page;
            else
                Pages.Add(pageType, page);
        }

        public Task NavigateHome(CancellationToken cancellationToken)
        {
            while (History.Count > 1)
                History.Pop();

            Console.Clear();
            return CurrentPage.Display(cancellationToken);
        }

        public T SetPage<T>() where T : Page
        {
            var pageType = typeof(T);

            if (CurrentPage != null && CurrentPage.GetType() == pageType)
                return CurrentPage as T;

            // leave the current page

            // select the new page
            if (!Pages.TryGetValue(pageType, out var nextPage))
                throw new KeyNotFoundException("The given page \"{0}\" was not present in the program".Format(pageType));

            // enter the new page
            History.Push(nextPage);

            return CurrentPage as T;
        }

        public async Task<T> NavigateTo<T>(CancellationToken cancellationToken) where T : Page
        {
            SetPage<T>();

            Console.Clear();
            await CurrentPage.Display(cancellationToken);
            return CurrentPage as T;
        }

        public async Task<Page> NavigateBack(CancellationToken cancellationToken)
        {
            History.Pop();

            Console.Clear();
            await CurrentPage.Display(cancellationToken);
            return CurrentPage;
        }
    }
}
