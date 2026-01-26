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
            Routing.RegisterRoute("public-event-details", typeof(PublicEventDetailsPage));


            Routing.RegisterRoute("add-note", typeof(NotePage));

            Routing.RegisterRoute("subjects", typeof(SubjectsPage));
            Routing.RegisterRoute("subject-details", typeof(SubjectDetailsPage));

            Routing.RegisterRoute("admin-users", typeof(AdminUsersPage));
            Routing.RegisterRoute("admin-add-class", typeof(AdminAddClassPage));
            Routing.RegisterRoute("admin-add-user", typeof(AdminAddUserPage));
            Routing.RegisterRoute("admin-change-role", typeof(AdminChangeRolePage));
        }
    }
}

