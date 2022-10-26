using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string NameLesson { get; set; }
        public Discipline IdDiscipline { get; set; }
        public TypeLesson IdTypeLesson { get; set; }
        public DateTime Date { get; set; }
        
        public List<Score> IdScore { get; set; }

    }
}
