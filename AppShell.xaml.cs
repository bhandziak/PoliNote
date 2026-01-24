using PoliNote.Views;

namespace PoliNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("add-private-event", typeof(AddPrivateEventPage));
        }
    }
}

