using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Score
    {
        public int Id { get; set; }
        public string Evalution { get; set; }
        public Users IdStudent { get; set; }
        public bool Attendance { get; set; }
        public Lesson IdLesson { get; set; }
    }
}
