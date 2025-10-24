using System.Windows;

namespace TP1
{
    /// <summary>
    /// Logique d'interaction pour ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window
    {
        public ManageUsersWindow()
        {
            InitializeComponent();
            AfficherTout();
            BtnAddTeacher.Click += BtnAddTeacher_Click;
            BtnAddStudent.Click += BtnAddStudent_Click;
            BtnDelete.Click += BtnDelete_Click;
        }

        public void AfficherTout()
        {
            cmbType.Items.Add("All");
            cmbType.Items.Add("Teacher");
            cmbType.Items.Add("Student");
            cmbType.Items.Add("Admin");
            if (cmbType.SelectedIndex == 1)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    lsvUser.Items.Add(t.ToString());
                }
                foreach (Student s in App.Current.Students.Values)
                {
                    lsvUser.Items.Add(s.ToString());
                }
                lsvUser.Items.Add(App.Current.Admin.ToString());
            }
            else if (cmbType.SelectedIndex == 2)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    lsvUser.Items.Add(t.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 3)
            {
                foreach (Student s in App.Current.Students.Values)
                {
                    lsvUser.Items.Add(s.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 4)
            {
                lsvUser.Items.Add(App.Current.Admin.ToString());
            }





        }

        private void cmbType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbType.SelectedIndex == 1)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    lsvUser.Items.Add(t.ToString());
                }
                foreach (Student s in App.Current.Students.Values)
                {
                    lsvUser.Items.Add(s.ToString());
                }
                lsvUser.Items.Add(App.Current.Admin.ToString());
            }
            else if (cmbType.SelectedIndex == 2)
            {
                foreach (Teacher t in App.Current.Teachers.Values)
                {
                    lsvUser.Items.Add(t.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 3)
            {
                foreach (Student s in App.Current.Students.Values)
                {
                    lsvUser.Items.Add(s.ToString());
                }
            }
            else if (cmbType.SelectedIndex == 4)
            {
                lsvUser.Items.Add(App.Current.Admin.ToString());
            }
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
            foreach (Student s in App.Current.Students.Values)
            {

            }
            foreach (Teacher t in App.Current.Teachers.Values)
            {

            }
        }
    }
}
