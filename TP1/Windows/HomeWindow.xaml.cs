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
        public HomeWindow()
        {
            InitializeComponent();
            Loaded += HomeWindow_Loaded;
        }

        private void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Header
            if (App.Current.LoggedInUser != null)
                txtUserName.Text = $"{App.Current.LoggedInUser.FirstName} {App.Current.LoggedInUser.LastName}";

            // Sessions + "All"
            var sessions = App.Current.Courses.Select(c => c.Semester).Distinct().ToList();
            cbSessions.ItemsSource = sessions.Cast<object>().Prepend("All").ToList();
            cbSessions.SelectedIndex = 0;

            LoadNewsCards();
        }

        private void cbSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.Current.LoggedInUser == null) return;

            var sel = cbSessions.SelectedItem;
            bool isAll = sel is string s && s == "All";
            Semester? sem = (!isAll && sel is Semester sv) ? sv : (Semester?)null;

            var user = App.Current.LoggedInUser;
            var courses =
                user is Teacher tch
                    ? App.Current.Courses.Where(c => c.TeacherId == tch.Id && (isAll || c.Semester.Equals(sem))).ToList()
                : user is Student stu
                    ? App.Current.Courses.Where(c => c.StudentIds.Contains(stu.Id) && (isAll || c.Semester.Equals(sem))).ToList()
                : Enumerable.Empty<Course>().ToList();

            lstCourses.ItemsSource = courses;
        }

        private void lstCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCourses.SelectedItem is not Course selected || App.Current.LoggedInUser == null) return;

            if (App.Current.LoggedInUser is Student)
                new CourseStudentWindow(selected).Show();
            else if (App.Current.LoggedInUser is Teacher)
                new CourseTeacherWindow(selected).Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) => App.Current.Logout();

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("pas fait", "Info",
            // MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("pas fait", "Info",
            //  MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        // noluvelles
        private void LoadNewsCards()
        {
            if (wpNews == null) return;
            wpNews.Children.Clear();

            foreach (var n in App.Current.News)
            {
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

                // image
                var img = new Image { Stretch = Stretch.UniformToFill };
                var bmp = TryLoadNewsImage(n);
                if (bmp != null) img.Source = bmp;
                Grid.SetRow(img, 0);
                grid.Children.Add(img);

                // title
                var title = new TextBlock
                {
                    Text = n.Title,
                    Margin = new Thickness(12, 10, 12, 0),
                    FontWeight = FontWeights.SemiBold,
                    TextTrimming = TextTrimming.CharacterEllipsis
                };
                Grid.SetRow(title, 1);
                grid.Children.Add(title);

                // date
                var date = new TextBlock
                {
                    Text = n.Date.ToString("d MMMM yyyy"),
                    Margin = new Thickness(12, 4, 12, 0),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8895A7"))
                };
                Grid.SetRow(date, 2);
                grid.Children.Add(date);

                card.Child = grid;
                wpNews.Children.Add(card);
            }
        }

        private BitmapImage? TryLoadNewsImage(News n)
        {
            var prop = n.GetType().GetProperty("ImagePath");
            var path = prop?.GetValue(n) as string;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] candidates = string.IsNullOrWhiteSpace(path)
                ? new[]
                {
                    System.IO.Path.Combine(baseDir, "Assets", "Images", "news01.jpg"),
                    System.IO.Path.Combine(baseDir, "Assets", "Images", "news01.png")
                }
                : new[]
                {
                    System.IO.Path.IsPathRooted(path) ? path : System.IO.Path.Combine(baseDir, path),
                    System.IO.Path.Combine(baseDir, "Assets", "Images", path)
                };

            foreach (var c in candidates)
            {
                try
                {
                    if (File.Exists(c))
                    {
                        var bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        bmp.UriSource = new Uri(c, UriKind.Absolute);
                        bmp.EndInit();
                        bmp.Freeze();
                        return bmp;
                    }
                }
                catch { /* ignore */ }
            }
            return null;
        }
    }
}
