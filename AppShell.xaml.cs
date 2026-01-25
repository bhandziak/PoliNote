using PoliNote.Views;

namespace PoliNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("add-private-event", typeof(AddPrivateEventPage));
            Routing.RegisterRoute("add-public-event", typeof(AddPublicEventPage));

            Routing.RegisterRoute("private-event-details", typeof(PrivateEventDetailsPage));
            Routing.RegisterRoute("add-note", typeof(NotePage));

            Routing.RegisterRoute("subjects", typeof(SubjectsPage));
            Routing.RegisterRoute("subject-details", typeof(SubjectDetailsPage));
        }
    }
}

