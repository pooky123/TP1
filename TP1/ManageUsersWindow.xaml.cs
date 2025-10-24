using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window
    {
        public List<String> users = new();
        public ManageUsersWindow()
        {
            InitializeComponent();
            AfficherUserType();
            BtnAddTeacher.Click += BtnAddTeacher_Click;
            BtnAddStudent.Click += BtnAddStudent_Click;
            BtnDelete.Click += BtnDelete_Click;
        }

        public void AfficherUserType()
        {
            cmbType.Items.Add("All");
            cmbType.Items.Add("Teacher");
            cmbType.Items.Add("Student");
            cmbType.Items.Add("Admin");
        }

        private void cmbType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AfficherUser();
        }

        private void BtnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            AddTeacherWindow addTeacherWindow = new();
            addTeacherWindow.ShowDialog();
        }
        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = new();
            addStudentWindow.ShowDialog();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultat = MessageBox.Show("Voulez-vous vraiment supprimer cette utilisateur", "Valide suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultat == MessageBoxResult.Yes)
            {
                foreach (Student s in App.Current.Students.Values)
                {
                    if (lsvUser.SelectedItem.ToString() == s.ToString())
                    {
                        deleteStudent(s);
                    }
                }
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    if (lsvUser.SelectedItem.ToString() == t.ToString())
                    {
                        deleteTeacher(t);
                    }
                }
                AfficherUser();
            }
        }


        private void AfficherUser()
        {
            lsvUser.Items.Clear();
            users.Clear();
            if (cmbType.SelectedIndex == 0)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    users.Add(t.ToString());
                }
                foreach (Student s in App.Current.Students.Values)
                {
                    users.Add(s.ToString());
                }
                users.Add(App.Current.Admin.ToString());
            }
            else if (cmbType.SelectedIndex == 1)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    users.Add(t.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 2)
            {
                foreach (Student s in App.Current.Students.Values)
                {
                    users.Add(s.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 3)
            {
                users.Add(App.Current.Admin.ToString());
            }
            foreach (string s in users)
            {
                lsvUser.Items.Add(s);
            }
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lsvUser.Items.Clear();
            foreach (string s in users)
            {
                if (s.Contains(txtSearch.Text))
                {
                    lsvUser.Items.Add(s);
                }
            }
        }

        private void deleteTeacher(Teacher t)
        {
            foreach (Course c in App.Current.Courses)
            {
                if (c.TeacherId == t.Id)
                {
                    c.TeacherId = -1;
                }
            }
            App.Current.Teachers.Remove(t.Id);
        }

        private void deleteStudent(Student s)
        {
            foreach (Course c in App.Current.Courses)
            {
                if (c.StudentIds.Contains(s.Id))
                {
                    c.StudentIds.Remove(s.Id);
                }
            }
            App.Current.Students.Remove(s.Id);
        }
    }
}
