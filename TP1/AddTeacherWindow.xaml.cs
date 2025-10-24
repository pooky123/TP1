using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour AddTeacherWindow.xaml
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        public AddTeacherWindow()
        {
            InitializeComponent();
            btnAddUser.Click += btnAddUser_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var falseId = false;
            if (txtBoxId.Text != "" && txtBoxFirstName.Text != "" && txtBoxLastName.Text != "")
            {
                falseId = false;
                if (int.TryParse(txtBoxId.Text, out int id))
                {
                    foreach (Teacher t in App.Current.Teachers.Values)
                    {
                        if (t.Id == id)
                        {
                            falseId = true;
                        }
                    }
                    if (!falseId)
                    {
                        Teacher newTeacher = new()
                        {
                            Id = id,
                            FirstName = txtBoxFirstName.Text,
                            LastName = txtBoxLastName.Text,
                        };
                        App.Current.Teachers.Add(newTeacher.Id, newTeacher);
                    }
                    else
                    {
                        MessageBox.Show("Le id est déjà utilisé");
                    }
                }
                else
                {
                    MessageBox.Show("Le id doit être en chiffre");
                }
            }
            else
            {
                MessageBox.Show("Tout les champs doit être remplie");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Logout();
        }
    }
}
