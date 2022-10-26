using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILessonRepository
    {
        void CreateLesson(Lesson lesson);
        void DeleteLesson(Lesson lesson);
        public void DeleteLessons(List<Lesson> lesson);
        void EditLesson(Lesson lesson);

        IEnumerable<Lesson> GetAllLessons(bool includes=false);

        Lesson GetLessonsById(int id, bool includes=false);
        Lesson GetLessonsByName(string name, bool includes=false);
        public IEnumerable<Lesson> GetAllLessonsByDiscipline(int idDiscipline);
        public IEnumerable<Lesson> GetAllLessonsNotAsNoTracking(bool includes = false);

    }
}
