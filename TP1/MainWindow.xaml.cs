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
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            // TODO: effectuer la connexion ici par le bouton ou par la touche Enter
            // App.Current.LoggedInUser = ...
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (rdbStudent.IsChecked == true)
            {
                foreach (Student s in App.Current.Students.Values)
                {

                }
            }

        }
    }
}
