using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour PasswordResetWindow.xaml
    /// </summary>
    public partial class PasswordResetWindow : Window
    {
        public PasswordResetWindow()
        {
            InitializeComponent();
            btnReset.Click += btnReset_Click;
            btnCancel.Click += btnCancel_Click;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment reset le password", "Valide reset", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultat == MessageBoxResult.Yes)
            {
                var infoOk = false;
                User u = new();
                foreach (Student s in App.Current.Students.Values)
                {
                    if (int.TryParse(txtBoxId.Text, out int id))
                    {
                        if (id == s.Id && txtBoxFirstName.Text == s.FirstName && txtBoxLastName.Text == s.LastName)
                        {
                            s.Password = (s.FirstName.Substring(0, 1) + s.LastName).ToLower();
                            infoOk = true;
                        }
                    }
                }
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    if (int.TryParse(txtBoxId.Text, out int id))
                    {
                        if (id == t.Id && txtBoxFirstName.Text == t.FirstName && txtBoxLastName.Text == t.LastName)
                        {
                            t.Password = (t.FirstName.Substring(0, 1) + t.LastName).ToLower();
                            infoOk = true;
                        }
                    }
                }
                if (infoOk)
                {
                    infoOk = false;
                    MessageBox.Show("Le mot de passe à été reset");
                }
                else
                {
                    MessageBox.Show("Ce user est introuvable. Tout les champ doivent être remplie");
                }

            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Logout();
        }
    }
}
