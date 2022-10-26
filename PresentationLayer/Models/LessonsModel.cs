using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class LessonsModel 
    {
        public Lesson Lesson { get; set; }
        public TypeLessonModel TypeLesson { get; set; }
        public DisciplineModel Discipline { get; set; }
        public List<ScoreModel> Score { get; set; }
    }
}
