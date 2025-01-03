using WayofLife.Pages;

namespace WayofLife
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddJournal), typeof(AddJournal));
        }
    }
}
