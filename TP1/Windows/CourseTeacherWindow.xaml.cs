using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TP1
{
    public partial class CourseTeacherWindow : Window
    {
        private readonly Course _course;

        private class EvalResultRow
        {
            public string EvalName { get; set; } = "";
            public int StudentId { get; set; }
            public string StudentName { get; set; } = "";
            public int Result { get; set; }
            public int Value { get; set; }
            public int Weight => Value; // for template readability
        }

        private class StudentAllRow
        {
            public string Name { get; set; } = "";
            public int Value { get; set; }
            public int Result { get; set; }
        }

        public CourseTeacherWindow(Course course)
        {
            InitializeComponent();
            _course = course ?? throw new ArgumentNullException(nameof(course));
            Loaded += OnLoaded;
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            lblTitle.Text = $"{_course.Id} - gr. {_course.Group} - {_course.Name}";
            cbEvaluations.ItemsSource = _course.Evaluations;
            cbEvaluations.DisplayMemberPath = "Name";
            if (_course.Evaluations.Count > 0) cbEvaluations.SelectedIndex = 0;
            RefreshCourseWeightTotal();
        }

        private void RefreshCourseWeightTotal()
        {
        }

        // eval select
        private void cbEvaluations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ev = cbEvaluations.SelectedItem as Evaluation;
            if (ev == null) return;

            txtWeight.Text = ev.Value.ToString();

            // Build rows
            var rows = new List<EvalResultRow>();
            foreach (var sid in _course.StudentIds)
            {
                App.Current.Students.TryGetValue(sid, out var stu);
                ev.StudentResults.TryGetValue(sid, out var res);

                rows.Add(new EvalResultRow
                {
                    EvalName = ev.Name,
                    StudentId = sid,
                    StudentName = stu != null ? $"{stu.FirstName} {stu.LastName}" : "(Unknown)",
                    Result = res,
                    Value = ev.Value
                });
            }
            lstEvalResults.ItemsSource = rows;

            // average
            var percents = rows.Where(r => ev.Value > 0)
                               .Select(r => r.Result * 100.0 / ev.Value)
                               .ToList();
            txtAverage.Text = percents.Count == 0 ? "—" : $"{percents.Average():0.00}";
        }

        // ponderation
        private void SaveWeight()
        {
            var ev = cbEvaluations.SelectedItem as Evaluation;
            if (ev == null) return;

            if (!int.TryParse(txtWeight.Text, out int newVal) || newVal < 0)
            {
                MessageBox.Show("Enter a valid non-negative weight.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ev.Value = newVal;
            cbEvaluations_SelectionChanged(cbEvaluations, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, null, null));
        }

        private void btnAddEvaluation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EvaluationAddWindow à brancher (Étudiant 2).", "Info");
        }

        private void btnAddResult_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AddResultWindow à brancher (Étudiant 2).", "Info");
        }

        private void lstEvalResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstEvalResults.SelectedItem is not EvalResultRow row) return;
            txtStudentId.Text = row.StudentId.ToString();
        }

        private void txtStudentId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(txtStudentId.Text, out int sid)) { txtFirstName.Text = ""; txtLastName.Text = ""; lstStudentAll.ItemsSource = null; return; }

            if (!App.Current.Students.TryGetValue(sid, out var stu)) { txtFirstName.Text = ""; txtLastName.Text = ""; lstStudentAll.ItemsSource = null; return; }

            txtFirstName.Text = stu.FirstName;
            txtLastName.Text = stu.LastName;

            var rows = _course.Evaluations.Select(ev =>
            {
                ev.StudentResults.TryGetValue(sid, out var r);
                return new StudentAllRow { Name = ev.Name, Value = ev.Value, Result = r };
            }).ToList();

            lstStudentAll.ItemsSource = rows;
        }

        private void NumericOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
