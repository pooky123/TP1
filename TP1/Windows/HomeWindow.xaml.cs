using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace TP1
{
    public partial class HomeWindow : Window
    {
        private sealed class CourseRow
        {
            public string Id { get; init; } = "";
            public string Name { get; init; } = "";
            public int Group { get; init; }
            public Course Ref { get; init; } = null!;
        }

        public HomeWindow()
        {
            InitializeComponent();
            Loaded += HomeWindow_Loaded;
        }

        private void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.Current.LoggedInUser != null)
                txtUserName.Text = $"{App.Current.LoggedInUser.FirstName} {App.Current.LoggedInUser.LastName}";

            var sessions = App.Current.Courses.Select(c => c.Semester).Distinct().ToList();
            var list = new List<object> { "All" };
            list.AddRange(sessions.Cast<object>());
            cbSessions.ItemsSource = list;
            cbSessions.SelectedIndex = 0;
            PopulateCourses(null);

            LoadNewsCards();
        }

        private void cbSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.Current.LoggedInUser == null) return;

            var sel = cbSessions.SelectedItem;
            if (sel is string) { PopulateCourses(null); return; }
            if (sel is Semester sem) { PopulateCourses(sem); return; }
        }

        private void PopulateCourses(Semester? filter)
        {
            if (App.Current.LoggedInUser == null) { lstCourses.ItemsSource = null; return; }

            bool MatchSemester(Course c) => !filter.HasValue || c.Semester.Equals(filter.Value);

            var items =
                App.Current.LoggedInUser is Teacher tch
                    ? App.Current.Courses.Where(c => c.TeacherId == tch.Id && MatchSemester(c))
                    : App.Current.LoggedInUser is Student stu
                        ? App.Current.Courses.Where(c => c.StudentIds.Contains(stu.Id) && MatchSemester(c))
                        : Enumerable.Empty<Course>();
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // ERREUR
            // TODO: CHANGEMENT
            // lstCourses.ItemsSource = items
            //   .Select(c => new CourseRow { Id = c.Id, Name = c.Name, Group = c.Group, Ref = c })
            //   .ToList();
            PasswordChangeWindow changeWindow = new();
            changeWindow.ShowDialog();
        }

        private void lstCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCourses.SelectedItem is null || App.Current.LoggedInUser is null) return;

            var course = lstCourses.SelectedItem switch
            {
                TP1.HomeWindow.CourseRow r => r.Ref,
                Course c => c,
                _ => null
            };
            if (course is null) return;

            try
            {
                if (App.Current.LoggedInUser is Student)
                    new CourseStudentWindow(course).Show();
                else if (App.Current.LoggedInUser is Teacher)
                    new CourseTeacherWindow(course).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }


        private void btnLogout_Click(object sender, RoutedEventArgs e) => App.Current.Logout();
        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordResetWindow resetWindow = new();
            resetWindow.ShowDialog();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true }); e.Handled = true; }

        private void LoadNewsCards()
        {
            if (wpNews == null) return;
            wpNews.Children.Clear();

            for (int i = 0; i < App.Current.News.Count; i++)
            {
                var n = App.Current.News[i];

                var card = new Border
                {
                    Width = 360,
                    Height = 260,
                    CornerRadius = new CornerRadius(10),
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E3E9F1")),
                    BorderThickness = new Thickness(1),
                    Background = Brushes.White,
                    Margin = new Thickness(0, 0, 16, 16)
                };

                var grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(140) });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var img = new Image { Stretch = Stretch.UniformToFill };
                var bmp = TryLoadNewsImageByName(n.ImageName) ?? TryLoadIndexedNewsImage(i);
                if (bmp != null) img.Source = bmp;
                Grid.SetRow(img, 0);
                grid.Children.Add(img);

                var title = new TextBlock { Text = n.Title, Margin = new Thickness(12, 10, 12, 0), FontWeight = FontWeights.SemiBold, TextTrimming = TextTrimming.CharacterEllipsis };
                Grid.SetRow(title, 1);
                grid.Children.Add(title);

                var date = new TextBlock { Text = n.Date.ToString("d MMMM yyyy"), Margin = new Thickness(12, 4, 12, 0), Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8895A7")) };
                Grid.SetRow(date, 2);
                grid.Children.Add(date);

                card.Child = grid;
                wpNews.Children.Add(card);
            }
        }

        private BitmapImage? TryLoadNewsImageByName(string? imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return null;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath1 = System.IO.Path.Combine(baseDir, "Assets", "Images", imageName);
            string filePath2 = System.IO.Path.Combine(baseDir, "Assets", "Images", imageName.ToLowerInvariant());

            var bmp = LoadBitmapIfExists(filePath1) ?? LoadBitmapIfExists(filePath2);
            if (bmp != null) return bmp;

            try
            {
                var pack = new Uri($"pack://application:,,,/Assets/Images/{imageName}", UriKind.Absolute);
                var b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = pack;
                b.EndInit();
                b.Freeze();
                return b;
            }
            catch { return null; }
        }

        private BitmapImage? TryLoadIndexedNewsImage(int indexZeroBased)
        {
            int n = indexZeroBased + 1;
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string num2 = n.ToString("D2");

            string[] candidates =
            {
                System.IO.Path.Combine(baseDir, "Assets", "Images", $"news{num2}.jpg"),
                System.IO.Path.Combine(baseDir, "Assets", "Images", $"news{num2}.png"),
                System.IO.Path.Combine(baseDir, "Assets", "Images", $"news{num2}.jpeg")
            };

            foreach (var c in candidates)
            {
                var bmp = LoadBitmapIfExists(c);
                if (bmp != null) return bmp;
            }
            try
            {
                var pack = new Uri($"pack://application:,,,/Assets/Images/news{num2}.jpg", UriKind.Absolute);
                var b = new BitmapImage();
                b.BeginInit();
                b.CacheOption = BitmapCacheOption.OnLoad;
                b.UriSource = pack;
                b.EndInit();
                b.Freeze();
                return b;
            }
            catch { return null; }
        }

        private static BitmapImage? LoadBitmapIfExists(string path)
        {
            try
            {
                if (!File.Exists(path)) return null;
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(path, UriKind.Absolute);
                bmp.EndInit();
                bmp.Freeze();
                return bmp;
            }
            catch { return null; }
        }
    }
}
