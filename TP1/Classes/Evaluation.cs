using System.Collections.Generic;
using System.Linq;

namespace TP1
{
    // Evaluation represente un travail ou un examen noté que l'étudiant effectue durant la session (exemple TP1 est une Evaluation)
    public class Evaluation
    {
        public string Name = "";    // TP1
        public int Value;           // Valeur de l'evaluation dans le bulletin (exemple 10 pour 10%)
        public Dictionary<int, int> StudentResults = new Dictionary<int, int>(); // <Id, Result>
    }
}
