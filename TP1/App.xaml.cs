using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TP1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current { get { return (App)Application.Current; } }
        public T GetWindow<T>() { return Windows.OfType<T>().First(); }

        public User? LoggedInUser;

        public List<Course> Courses { get => _courses; }
        private readonly List<Course> _courses = new List<Course>()
        {
            new Course()
            {
                Id = "1C5",
                Name = "Ordinateurs et systèmes d'exploitation",
                Semester = Semester.A21,
                Group = 1,
                CourseLessons = new List<Lesson>()
                {
                    new Lesson() { DayOfWeek = DayOfWeek.Tuesday, PeriodStart = 7, PeriodLength = 2, ClassroomId = "D139" },
                    new Lesson() { DayOfWeek = DayOfWeek.Thursday, PeriodStart = 0, PeriodLength = 3, ClassroomId = "D139" },
                },
                TeacherId = 1001,
                StudentIds = new List<int>()
                {
                    21000123,
                    21000456,
                    21000789,
                    19887766,
                    19559977,
                    18996644,
                },
                Evaluations = new List<Evaluation>()
                {
                    new Evaluation() {
                        Name = "Exam 1",
                        Value = 20,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 15 },
                            { 21000456, 10 },
                            { 21000789, 5 },
                            { 19887766, 5 },
                            { 19559977, 10 },
                            { 18996644, 15 },
                        }
                    },
                    new Evaluation() {
                        Name = "TP 1",
                        Value = 30,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 25 },
                            { 21000456, 20 },
                            { 21000789, 15 },
                            { 19887766, 15 },
                            { 19559977, 20 },
                            { 18996644, 25 },
                        }
                    },
                    new Evaluation() {
                        Name = "Exam 2",
                        Value = 30,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 25 },
                            { 21000456, 20 },
                            { 21000789, 15 },
                            { 19887766, 15 },
                            { 19559977, 20 },
                            { 18996644, 25 },
                        }
                    }
                }
            },
            new Course()
            {
                Id = "3C4",
                Name = "Programmation d'interfaces graphiques",
                Semester = Semester.A21,
                Group = 1,
                CourseLessons = new List<Lesson>()
                {
                    new Lesson() { DayOfWeek = DayOfWeek.Monday, PeriodStart = 5, PeriodLength = 2, ClassroomId = "D134" },
                    new Lesson() { DayOfWeek = DayOfWeek.Wednesday, PeriodStart = 2, PeriodLength = 2, ClassroomId = "D134" },
                },
                TeacherId = 1001,
                StudentIds = new List<int>()
                {
                    21000123,
                    21000456,
                    21000789,
                    19887766,
                    19559977,
                    18996644,
                },
                Evaluations = new List<Evaluation>()
                {
                    new Evaluation() {
                        Name = "Exam 1",
                        Value = 50,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 35 },
                            { 21000456, 30 },
                            { 21000789, 25 },
                            { 19887766, 25 },
                            { 19559977, 30 },
                            { 18996644, 25 },
                        }
                    },
                    new Evaluation() {
                        Name = "Exam 2",
                        Value = 50,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 45 },
                            { 21000456, 40 },
                            { 21000789, 35 },
                            { 19887766, 35 },
                            { 19559977, 40 },
                            { 18996644, 45 },
                        }
                    }
                }
            },
            new Course()
            {
                Id = "3C4",
                Name = "Programmation d'interfaces graphiques",
                Semester = Semester.A21,
                Group = 2,
                CourseLessons = new List<Lesson>()
                {
                    new Lesson() { DayOfWeek = DayOfWeek.Monday, PeriodStart = 2, PeriodLength = 2, ClassroomId = "D136" },
                    new Lesson() { DayOfWeek = DayOfWeek.Wednesday, PeriodStart = 9, PeriodLength = 2, ClassroomId = "D134" },
                },
                TeacherId = 1001,
                StudentIds = new List<int>()
                {
                    21000123,
                    21000456,
                    21000789,
                    19887766,
                    19559977,
                    18996644,
                },
                Evaluations = new List<Evaluation>()
                {
                    new Evaluation() {
                        Name = "Exam 1",
                        Value = 50,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 35 },
                            { 21000456, 30 },
                            { 21000789, 25 },
                            { 19887766, 25 },
                            { 19559977, 30 },
                            { 18996644, 25 },
                        }
                    },
                    new Evaluation() {
                        Name = "Exam 2",
                        Value = 50,
                        StudentResults = new Dictionary<int, int>
                        {
                            { 21000123, 45 },
                            { 21000456, 40 },
                            { 21000789, 35 },
                            { 19887766, 35 },
                            { 19559977, 40 },
                            { 18996644, 45 },
                        }
                    }
                }
            },
        };

        public Admin Admin = new Admin() { Id = 0, LastName = "Admin" };

        public Dictionary<int, Student> Students { get => _students; }
        private readonly Dictionary<int, Student> _students = new Dictionary<int, Student>()
        {
            { 21000123, new Student() { Id = 21000123, FirstName = "Paul", LastName = "Berube", Password = "pberube" } },
            { 21000456, new Student() { Id = 21000456, FirstName = "Mathieu", LastName = "Gagnon", Password = "mgagnon" } },
            { 21000789, new Student() { Id = 21000789, FirstName = "Robert", LastName = "Simard", Password = "rsimard" } },
            { 19887766, new Student() { Id = 19887766, FirstName = "Elise", LastName = "Huard", Password = "ehuard" } },
            { 19559977, new Student() { Id = 19559977, FirstName = "Marie", LastName = "Sauve", Password = "msauve" } },
            { 18996644, new Student() { Id = 18996644, FirstName = "Sylvie", LastName = "Michaud", Password = "smichaud" } },
        };

        public Dictionary<int, Teacher> Teachers { get => _teachers; }
        private readonly Dictionary<int, Teacher> _teachers = new Dictionary<int, Teacher>()
        {
            { 1001, new Teacher() { Id = 1001, FirstName = "Mael", LastName = "Perreault", Password = "mperreault" } },
            { 1002, new Teacher() { Id = 1002, FirstName = "Yannick", LastName = "Charron", Password = "ycharron" } },
            { 1003, new Teacher() { Id = 1003, FirstName = "Karine", LastName = "Moreau", Password = "kmoreau"  } },
            { 1004, new Teacher() { Id = 1004, FirstName = "Robert", LastName = "Turenne", Password = "rturenne" } },
            { 1005, new Teacher() { Id = 1005, FirstName = "Alain", LastName = "Martel", Password = "amartel" } },
        };

        public List<Period> Periods { get => periods; }
        private readonly List<Period> periods = new List<Period>()
        {
            new Period(){ StartTime = new TimeSpan(8, 0, 0) },
            new Period(){ StartTime = new TimeSpan(8, 55, 0) },
            new Period(){ StartTime = new TimeSpan(9, 50, 0) },
            new Period(){ StartTime = new TimeSpan(10, 45, 0) },
            new Period(){ StartTime = new TimeSpan(11, 40, 0) },
            new Period(){ StartTime = new TimeSpan(12, 35, 0) },
            new Period(){ StartTime = new TimeSpan(13, 30, 0) },
            new Period(){ StartTime = new TimeSpan(14, 25, 0) },
            new Period(){ StartTime = new TimeSpan(15, 20, 0) },
            new Period(){ StartTime = new TimeSpan(16, 15, 0) },
            new Period(){ StartTime = new TimeSpan(17, 10, 0) },
        };

        public List<News> News { get => _news; }
        private readonly List<News> _news = new List<News>()
        {
            new News()
            {
                Title = "Formulaire - Changement de programme - Tour 1",
                Date = new DateTime(2021, 9, 21),
                ImageName = "news01.jpg",
                Content = "À tous les élèves inscrits à la session d'automne 2021,\n\n" +
                "Si vous désirez changer de programme à la session d'hiver 2022, dans l'un de nos trois campus, vous devez remplir le formulaire de demande de changement de programmeavant le 1er novembre 2021 16h30 en copiant le lien suivant dans votre navigateur internet ; https://forms.office.com/r/AR86v2m1RSAssurez-vous \n\n" +
                "Assurez-vous d'avoir le ou les préalable(s) exigé(s) pour le programme de votre choix à l'hiver 2022 en vous référant au https://www.cstj.qc.ca/programmes/ \n\n" +
                "Pour toutes questions, vous pouvez communiquer avec votre API en vous rendant sur le module de prise de rendez-vous via votre portail Omnivox. \n\n" +
                "Veuillez prendre note que la réponse à votre demande vous sera communiquée auplus tardle 17novembre2021. \n\n" +
                "Si vous désirez faire une demande d'admission dans un autre cégep à la session d'hiver 2022, vous devez compléter une demande d'admission sur le site du SRAM au https://admission.sram.qc.ca/avant le 1ernovembre2021. \n\n" +
                "Bonne session !"
            },
            new News()
            {
                Title = "RAPPEL - Montant forfaitaire de 100 $ pour étudiants",
                Date = new DateTime(2021, 9, 21),
                ImageName = "news02.jpg",
                Content = "Un montant forfaitaire de 100 $ par session sera offert aux étudiants à temps plein de niveau collégial et universitaire, afin de pallier les inconvénients qu'ils ont subi durant les sessions d'automne 2020 et d'hiver 2021. \n\n" +
                "Les étudiants admissibles sont invités à remplir le formulaire de la demande du montant forfaitaire qui est disponible sur le portail Omnivox du Cégep de Saint-Jérôme. Le traitement des versements du montant forfaitaire se fait électroniquement depuis la mi-septembre 2021, en ordre de réception des demandes. Les versements ne sont pas immédiats et ils s'étendent sur plusieurs semaines. \n\n" +
                "Ce montant n'aura aucune incidence sur les autres formes d'aide financière aux études, par exemple les prêts et bourses. \n\n" +
                "Veuillez noter que les critères d'admissibilité au montant forfaitaire ont été élargis pour certaines catégories d'étudiants reconnues comme étudiant à temps plein par l'Aide financière aux études. \n\n" +
                "Info : https://www.quebec.ca/education/aide-financiere-aux-etudes/aide-financiere-etudiants-postsecondaire-covid-19/montant-forfaitaire-etudiant-covid-19#c96047 \n\n" +
                "Si un étudiant préfère remplir sa demande en format imprimable (PDF), il doit transmettre son document au registrariat de l'établissement par courriel à l'adresse montant_forfaitaire_registraire@cstj.qc.ca \n\n" +
                "Pour en savoir plus, on consulte la page Web du ministère : https://www.quebec.ca/index.php?id=11770"
            },
        };

        public void Logout()
        {
            var mainWindow = new MainWindow();

            for (int i = Current.Windows.Count - 1; i >= 0; i--)
            {
                if (Current.Windows[i] != mainWindow)
                    Current.Windows[i].Close();
            }

            mainWindow.Show();
        }
    }
}
