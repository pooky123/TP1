using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TP1
{
    public partial class AddResultWindow : Window
    {
        private readonly Course _course;
        public AddResultWindow(Course course, Evaluation? selected = null)
        {
            _course = course ?? throw new ArgumentNullException(nameof(course));
            InitializeComponent();
            cbEval.ItemsSource = _course.Evaluations;
            cbEval.DisplayMemberPath = "Name";
            if (selected != null) cbEval.SelectedItem = selected;
            txtId.TextChanged += TxtId_TextChanged;
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text) if (ch < '0' || ch > '9') { e.Handled = true; break; }
        }

        private void TxtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(txtId.Text, out var id)) { txtFirst.Text = ""; txtLast.Text = ""; return; }
            if (!App.Current.Students.TryGetValue(id, out var s) || !_course.StudentIds.Contains(id)) { txtFirst.Text = ""; txtLast.Text = ""; return; }
            txtFirst.Text = s.FirstName;
            txtLast.Text = s.LastName;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (cbEval.SelectedItem is not Evaluation ev) { MessageBox.Show("Select evaluation."); return; }
            if (!int.TryParse(txtId.Text, out var id) || !_course.StudentIds.Contains(id)) { MessageBox.Show("Invalid student."); return; }
            if (!int.TryParse(txtResult.Text, out var pts) || pts < 0) { MessageBox.Show("Invalid points."); return; }
            ev.StudentResults[id] = pts;
            DialogResult = true;
            Close();
        }
    }
}
