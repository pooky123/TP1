using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow()
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
                    foreach (Student s in App.Current.Students.Values)
                    {
                        if (s.Id == id)
                        {
                            falseId = true;
                        }
                    }
                    if (!falseId)
                    {
                        Student newStudent = new()
                        {
                            Id = id,
                            FirstName = txtBoxFirstName.Text,
                            LastName = txtBoxLastName.Text,
                        };
                        App.Current.Students.Add(newStudent.Id, newStudent);
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
