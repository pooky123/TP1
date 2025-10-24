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
            // --- Admin ---
            if (userId.Text == "Admin" && UserPW.Password == "Admin")
            {
                if (App.Current.Admin != null) App.Current.LoggedInUser = App.Current.Admin;

                new AdminWindow().Show();
                this.Close(); 
                return;
            }

            // Try to parse the typed ID once
            if (!int.TryParse(userId.Text, out int id))
            {
                MessageBox.Show("Please enter a numeric id.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // --- Student flow ---
            if (rdbStudent.IsChecked == true)
            {
                if (App.Current.Students.TryGetValue(id, out var s) && UserPW.Password == s.Password)
                {
                    App.Current.LoggedInUser = s;      
                    new HomeWindow().Show();           
                    this.Close();                      
                    return;
                }
            }

            // --- Teacher flow ---
            if (rdbTeacher.IsChecked == true)
            {
                if (App.Current.Teachers.TryGetValue(id, out var t) && UserPW.Password == t.Password)
                {
                    App.Current.LoggedInUser = t;    
                    new HomeWindow().Show();        
                    this.Close();                     
                    return;
                }
            }

            MessageBox.Show("User not found. Please verify Username, Password or user type.",
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
