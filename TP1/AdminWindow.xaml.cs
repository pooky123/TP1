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
            btnManageGroups.Click += btnManageGroups_Click;
            btnManageLessons.Click += btnManageLessons_Click;
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnManageGroups_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnManageLessons_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
