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

            //btnStudent.Click += btnStudent_Click;
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

        private void btnStudent_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
