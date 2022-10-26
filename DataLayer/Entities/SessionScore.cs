using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class SessionScore
    {
        public int Id { get; set; }
        public Discipline IdDiscipline { get; set; }
        public DateTime Date { get; set; }
        public int ScoreSession { get; set; }
        public Users IdStudent { get; set; }
    }
}
