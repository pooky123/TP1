using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
            btnChange.Click += btnChange_Click;
            btnCancel.Click += btnCancel_Click;
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment reset le password", "Valide reset", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultat == MessageBoxResult.Yes)
            {
                if (txtBoxOld.Text != "" && txtBoxNew.Text != "" && txtBoxConf.Text != "")
                {
                    if (App.Current.LoggedInUser.Password == txtBoxOld.Text)
                    {
                        if (txtBoxNew.Text == txtBoxConf.Text)
                        {
                            foreach (Student s in App.Current.Students.Values)
                            {
                                if (s.Id == App.Current.LoggedInUser.Id)
                                {
                                    s.Password = txtBoxNew.Text;
                                }
                            }
                            foreach (Teacher t in App.Current.Teachers.Values)
                            {
                                if (t.Id == App.Current.LoggedInUser.Id)
                                {
                                    t.Password = txtBoxNew.Text;
                                }
                            }
                            MessageBox.Show("Le mot de passe à été changé");
                        }
                        else
                        {
                            MessageBox.Show("Nouveau mot de passe et confirmation doit être les mêmes");
                        }
                    }
                    else
                    {
                        MessageBox.Show("L'encien mot de passe n'est pas valide");
                    }
                }
                else
                {
                    MessageBox.Show("Tout les champs doivent être remplie");
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Logout();
        }
    }
}
