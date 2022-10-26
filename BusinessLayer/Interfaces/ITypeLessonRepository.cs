using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITypeLessonRepository
    {
        public IEnumerable<TypeLesson> GetAllTypeLessons();
        public TypeLesson GetTypeLessonById(int id);
        public TypeLesson GetTypeLessonByName(string Name);
    }
}
