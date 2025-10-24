using System.Windows;
using System.Windows.Input;

namespace TP1
{

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Débuter avec un user non connecté
            App.Current.LoggedInUser = null;

            btnLogin.Click += btnLogin_Click;
            btnForgotPW.Click += btnForgotPW_Click;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
        private void btnForgotPW_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new();
            adminWindow.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (userId.Text == "Admin" && UserPW.Password == "Admin")
            {
                AdminWindow adminWindow = new();
                adminWindow.ShowDialog();
                userId.Clear();
                UserPW.Clear();
                return;

            }
            else if (rdbStudent.IsChecked == true)
            {
                foreach (Student s in App.Current.Students.Values)
                {
                    if (int.TryParse(userId.Text, out int id))
                    {
                        if (id == s.Id)
                        {
                            if (UserPW.Password == s.Password)
                            {
                                // Appeler Home window
                                userId.Clear();
                                UserPW.Clear();
                                return;
                            }
                        }
                    }
                }
            }
            else if (rdbTeacher.IsChecked == true)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    if (int.TryParse(userId.Text, out int id))
                    {
                        if (id == t.Id)
                        {
                            if (UserPW.Password == t.Password)
                            {
                                // Appeler Home widow
                                return;
                            }
                        }
                    }
                }
            }
            MessageBox.Show("User not found. Please verify Username, Password or user type.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
