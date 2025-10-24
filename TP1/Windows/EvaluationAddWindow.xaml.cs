using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace TP1
{
    public partial class EvaluationAddWindow : Window
    {
        private readonly Course _course;
        public EvaluationAddWindow(Course course)
        {
            _course = course ?? throw new ArgumentNullException(nameof(course));
            InitializeComponent();
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            foreach (var ch in e.Text) if (ch < '0' || ch > '9') { e.Handled = true; break; }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var name = txtName.Text?.Trim();
            if (string.IsNullOrEmpty(name)) { MessageBox.Show("Name required."); return; }
            if (!int.TryParse(txtWeight.Text, out int weight) || weight < 0) { MessageBox.Show("Invalid weight."); return; }

            var ev = new Evaluation { Name = name, Value = weight, StudentResults = new Dictionary<int, int>() };
            _course.Evaluations.Add(ev);
            DialogResult = true;
            Close();
        }
    }
}
