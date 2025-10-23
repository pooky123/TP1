using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TP1
{
    public partial class CourseStudentWindow : Window
    {
        private readonly Course _course;
        private readonly Student _student = null!; // set in ctor after type check

        private class ResultRow
        {
            public string EvalName { get; set; } = "";
            public int StudentId { get; set; }
            public string StudentFullName { get; set; } = "";
            public int Result { get; set; }  // points obtained
            public int Value { get; set; }   // weighting / max points
        }

        public CourseStudentWindow(Course course)
        {
            InitializeComponent();

            _course = course ?? throw new ArgumentNullException(nameof(course));

            if (App.Current.LoggedInUser is not Student s)
            {
                MessageBox.Show("Aucun étudiant connecté.", "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            _student = s;

            // Put the course name in the window title like your screenshot
            Title = $"{_course.Id} - gr. {_course.Group} - {_course.Name}";

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Identity fields
            txtId.Text = _student.Id.ToString();
            txtFirstName.Text = _student.FirstName;
            txtLastName.Text = _student.LastName;

            // Build rows only for evaluations where this student has a result (to match your screenshot)
            var rows = new List<ResultRow>();
            foreach (var ev in _course.Evaluations)
            {
                if (ev.StudentResults.TryGetValue(_student.Id, out var points))
                {
                    rows.Add(new ResultRow
                    {
                        EvalName = ev.Name,
                        StudentId = _student.Id,
                        StudentFullName = $"{_student.FirstName} {_student.LastName}",
                        Result = points,
                        Value = ev.Value
                    });
                }
            }

            lstResults.ItemsSource = rows;

            // Totals and percentage (sum only the evaluations that have a result)
            var totalPoints = rows.Sum(r => r.Result);
            var totalValue = rows.Sum(r => r.Value);
            var percent = totalValue > 0 ? totalPoints * 100.0 / totalValue : 0.0;

            txtTotal.Text = $"{totalPoints:0.00} / {totalValue}";
            txtPercent.Text = $"{percent:0.00}%";
        }
    }
}
