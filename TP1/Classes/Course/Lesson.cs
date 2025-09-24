using System;

namespace TP1
{
    // Lesson represente une periode de prestation d'un cours durant la session, (exemple Lundi 8h à 10h40 au D139)
    public class Lesson
    {
        public DayOfWeek DayOfWeek;     // DayOfWeek.Monday
        public int PeriodStart;         // Représente l'index de la période de début en partant de 0 (exemple Lundi 8h à 10h40 serait 0)
        public int PeriodLength;        // Représente le nombre de périodes (exemple Lundi 8h à 10h40 serait 3)
        public string ClassroomId;      // D139
    }
}
