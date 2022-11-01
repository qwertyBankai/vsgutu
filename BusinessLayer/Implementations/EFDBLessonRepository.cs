using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class EFDBLessonRepository : ILessonRepository
    {
        private EFDBContext context;
        public EFDBLessonRepository(EFDBContext context)
        {
            this.context = context;
        }
        public void CreateLesson(Lesson lesson)
        {
            context.Lesson.Add(lesson);
            context.SaveChanges();
        }

        public void DeleteLesson(Lesson lesson)
        {
            context.Lesson.Remove(lesson);
            context.SaveChanges();
        }

        public void DeleteLessons(List<Lesson> lesson)
        {
            foreach (var item in lesson)
            {
                context.Lesson.Remove(item);
            }
            context.SaveChanges();
        }

        public void EditLesson(Lesson lesson)
        {
            context.Entry(lesson).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public IEnumerable<Lesson> GetAllLessons(bool includes=false)
        {
            if (includes)
            {
                return context.Set<Lesson>().Include(x => x.IdDiscipline)
                    .Include(x => x.IdScore)
                    .Include(x => x.IdTypeLesson).AsNoTracking().ToList();
            }
            else
            {
                return context.Lesson.ToList(); 
            }
        }

        public IEnumerable<Lesson> GetAllLessonsNotAsNoTracking(bool includes = false)
        {
            if (includes)
            {
                return context.Set<Lesson>().Include(x => x.IdDiscipline).Include(x=>x.IdDiscipline.IdGroup)
                    .Include(x => x.IdScore)
                    .Include(x => x.IdTypeLesson).ToList();
            }
            else
            {
                return context.Lesson.ToList();
            }
        }

        public IEnumerable<Lesson> GetAllLessonsByDiscipline(int idDiscipline)
        {
            return context.Set<Lesson>().Include(x => x.IdDiscipline)
                     .Include(x => x.IdScore)
                     .Include(x => x.IdTypeLesson).Where(x=>x.IdDiscipline.Id == idDiscipline).ToList();
        }

        public Lesson GetLessonsById(int id, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Lesson>().Include(x => x.IdDiscipline)
                     .Include(x => x.IdScore)
                     .Include(x => x.IdTypeLesson).AsNoTracking().FirstOrDefault(x=> x.Id == id);
            }
            else
            {
                return context.Lesson.FirstOrDefault(x => x.Id == id);
            }
        }
        public Lesson GetLessonsByName(string name, bool includes = false)
        {
            if (includes)
            {
                return context.Set<Lesson>().Include(x => x.IdDiscipline)
                     .Include(x => x.IdScore)
                     .Include(x => x.IdTypeLesson).AsNoTracking().FirstOrDefault(x => x.NameLesson == name);
            }
            else
            {
                return context.Lesson.FirstOrDefault(x => x.NameLesson == name);
            }
        }
    }
}
