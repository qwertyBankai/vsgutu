using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class EK
    {
        public int Id { get; set; }
        public int EKScore { get; set; }
        public DateTime Date { get; set; }
        public Users IdStudent { get; set; }
        public Discipline IdDiscipline { get; set; }
    }
}
