using System.Collections.Generic;

namespace TP1
{
    // Course represente un cours que l'étudiant suit durant la session, (exemple 3C4 groupe 001 est un cours)
    public class Course
    {
        public string Id;
        public string Name;
        public Semester Semester;
        public int Group;
        public int TeacherId;
        public List<Lesson> CourseLessons = new List<Lesson>();         // Prestations de cours dans la semaine
        public List<int> StudentIds = new List<int>();                  // Etudiants qui sont presentement inscrit au cours
        public List<Evaluation> Evaluations = new List<Evaluation>();   // Evaluation du cours avec les resultats
    }
}
