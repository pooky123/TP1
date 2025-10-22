using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            btnManageUsers.Click += btnManageUsers_Click;
            btnQuitter.Click += btnQuitter_Click;
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersWindow usersWindow = new();
            usersWindow.ShowDialog();
        }
        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Logout();
        }
    }
}
